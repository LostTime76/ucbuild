using System.Data;
using Lua;
using Lua.Standard;

namespace UcBuild.Runtime;

/// <summary>
/// Provides the lua runtime for a build
/// </summary>
public sealed class LuaRuntime
{
	private const string _instanceReference = "__ref";

	private const string _targetCommand = "target";

	private const string _artifactRootDirectoryVariable = "artf_root";
	private const string _vscDirectoryVariable          = "vsc_dir";

	private const string _targetArtifactsDirectory = "artf_dir";
	private const string _targetConfigureCallback  = "configure";

	private readonly LuaState _state;

	private readonly Dictionary<string, LuaTable> _targets = [];

	/// <summary>
	/// Creates a new runtime and pre evaluates a project script
	/// </summary>
	/// <param name="scriptFilepath">
	/// The filepath of the script to pre evaluate
	/// </param>
	/// <param name="codeFragments">
	/// Code fragments to be executed before the main script
	/// </param>
	public LuaRuntime(string scriptFilepath, IEnumerable<string>? codeFragments = null)
	{
		_state                = CreateState(scriptFilepath, codeFragments);
		ArtifactRootDirectory = GetDirectory(_artifactRootDirectoryVariable);
		VscDirectory          = GetDirectory(_vscDirectoryVariable);
		Targets               = [.. _targets.Keys];
	}

	/// <summary>
	/// Creates and configures a target with the given name
	/// </summary>
	/// <param name="name">
	/// The name of the target to configure
	/// </param>
	/// <returns>
	/// The created target
	/// </returns>
	public BuildTarget ConfigureTarget(string name)
	{
		// Create a new target
		var target  = new BuildTarget() {
			Name              = name,
			ArtifactDirectory = Path.Combine(ArtifactRootDirectory, name)	
		};

		// Get the interop target
		var itarget = _targets[name];

		// Initialize the interop target
		itarget[_instanceReference]        = LuaValue.FromObject(target);
		itarget[_targetArtifactsDirectory] = target.ArtifactDirectory;

		// Attempt to configure the target
		if (itarget.TryGetValue(_targetConfigureCallback, out var value))
		{
			var function = value.Read<LuaFunction>();
			var _        = _state.Call(function, [itarget]).Result;
		}

		return target;
	}

	private LuaState CreateState(string scriptFilepath, IEnumerable<string>? codeFragments)
	{
		// Create the state
		var state = LuaState.Create();

		// Add standard libraries
		state.OpenStandardLibraries();

		// Add additional functions to the runtime
		state.AddGlobals([
			(_targetCommand, TargetCommand)
		]);

		// Evaluate any code fragments
		state.EvaluateFragments(codeFragments);

		// Evaluate the script
		state.EvaluateScript(scriptFilepath);

		return state;
	}

	private ValueTask<int> TargetCommand(
		LuaFunctionExecutionContext context, CancellationToken token)
	{
		var name = Path.GetFileNameWithoutExtension(context.GetArgument<string>(0));
		var _    = ModuleLibrary.Require(context, token);

		// The target table is at the top of the stack
		_targets[name] = context.State.Stack.Pop().Read<LuaTable>();

		return new(context.Return());
	}

	private string GetDirectory(string variable)
	{
		string? path = _state.GetGlobalString(variable);

		return string.IsNullOrEmpty(path) ?
			Directory.GetCurrentDirectory() : Path.GetFullPath(path);
	}

	/// <summary>
	/// Gets the path of the root directory where build artifacts are stored
	/// </summary>
	public string ArtifactRootDirectory { get; private init; }

	/// <summary>
	/// Gets the visual studio code directory
	/// </summary>
	public string VscDirectory { get; private init; }

	/// <summary>
	/// Gets the names of all the targets available within the project
	/// </summary>
	public HashSet<string> Targets { get; private init; }
}
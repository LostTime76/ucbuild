using Lua;
using Lua.Standard;

namespace UcBuild;

public sealed class BuildTarget
{
	private readonly LuaState        _state;
	private readonly List<string>    _vscDefines;
	private readonly List<string>    _compilerOptions;
	private readonly List<string>    _compilerIncludes;
	private readonly List<string>    _compilerDefines;
	private readonly List<string>    _linkerOptions;
	private readonly List<string>    _linkerLibraries;
	private readonly List<string>    _linkerObjects;
	private readonly HashSet<string> _sources;

	private string? _vscIntellisenseMode;

	private BuildTarget(string filepath, IEnumerable<string> fragments)
	{
		Name              = Path.GetFileNameWithoutExtension(filepath);
		_vscDefines       = [];
		_compilerOptions  = [];
		_compilerIncludes = [];
		_compilerDefines  = [];
		_linkerOptions    = [];
		_linkerLibraries  = [];
		_linkerObjects    = [];
		_sources          = [];
		_state            = CreateState();

		EvaluateFragments(fragments);
		Evaluate(filepath);
	}

	public static IEnumerable<BuildTarget> EvaluateTargets(
		IEnumerable<string> filepaths, IEnumerable<string> codeFragments)
	{
		var targets = new List<BuildTarget>();

		foreach(string filepath in filepaths)
		{
			targets.Add(new(filepath, codeFragments));
		}

		return targets;
	}

	private ValueTask<int> AddVscDefines(
		LuaFunctionExecutionContext context, CancellationToken token)
	{
		AddStrings(context, _vscDefines);
		return default;
	}

	private ValueTask<int> AddCompilerOptions(
		LuaFunctionExecutionContext context, CancellationToken token)
	{
		AddStrings(context, _compilerOptions);
		return default;
	}

	private ValueTask<int> AddCompilerIncludes(
		LuaFunctionExecutionContext context, CancellationToken token)
	{
		AddStrings(context, _compilerIncludes);
		return default;
	}

	private ValueTask<int> AddCompilerDefines(
		LuaFunctionExecutionContext context, CancellationToken token)
	{
		AddStrings(context, _compilerDefines);
		return default;
	}

	private ValueTask<int> AddLinkerOptions(
		LuaFunctionExecutionContext context, CancellationToken token)
	{
		AddStrings(context, _linkerOptions);
		return default;
	}

	private ValueTask<int> AddLinkerLibraries(
		LuaFunctionExecutionContext context, CancellationToken token)
	{
		AddStrings(context, _linkerLibraries);
		return default;
	}

	private ValueTask<int> AddLinkerObjects(
		LuaFunctionExecutionContext context, CancellationToken token)
	{
		AddStrings(context, _linkerObjects);
		return default;
	}

	private ValueTask<int> AddSources(
		LuaFunctionExecutionContext context, CancellationToken token)
	{
		// The first argument is the target source directory
		string directory = context.GetArgument<string>(0);

		// The rest of the arguments are source filenames within the directory
		foreach(LuaValue lvalue in context.Arguments[1..])
		{
			if (!lvalue.TryRead(out string filename))
			{
				continue;
			}

			_sources.Add(Path.GetFullPath(Path.Combine(directory, filename)));
		}

		return default;
	}

	private static void AddStrings(LuaFunctionExecutionContext context, List<string> strings)
	{
		foreach(LuaValue lvalue in context.Arguments)
		{
			if (lvalue.TryRead(out string value))
			{
				strings.Add(value);
			}
		}
	}

	private void EvaluateFragments(IEnumerable<string> fragments)
	{
		foreach(string fragment in fragments)
		{
			var _ = _state.DoStringAsync(fragment).Result;
		}
	}

	private void Evaluate(string filepath)
	{
		// Evaluate the script
		_state.EvaluateScript(filepath);

		// Resolve settings
		_vscIntellisenseMode = ResolveString("vsc_imode");
	}

	private string? ResolveString(string variable)
	{
		var value = _state.Environment[variable];

		return value.Type == LuaValueType.String ? value.Read<string>() : null;
	}

	private LuaState CreateState()
	{
		// Create the state
		var state = LuaState.Create();
		var env   = state.Environment;

		// Add the standard libraries
		state.OpenStandardLibraries();

		// Add variables
		env["target"] = Name;

		// Add custom functions
		env["vsc_defs"] = new LuaFunction(AddVscDefines);
		env["cc_opts"]  = new LuaFunction(AddCompilerOptions);
		env["cc_incs"]  = new LuaFunction(AddCompilerIncludes);
		env["cc_defs"]  = new LuaFunction(AddCompilerDefines);
		env["ld_opts"]  = new LuaFunction(AddLinkerOptions);
		env["ld_libs"]  = new LuaFunction(AddLinkerLibraries);
		env["ld_objs"]  = new LuaFunction(AddLinkerObjects);
		env["sources"]  = new LuaFunction(AddSources);

		return state;
	}

	public string Name { get; private init; }

	public string? VscIntellisenseMode => _vscIntellisenseMode;

	public IEnumerable<string> VscDefines => _vscDefines;

	public IEnumerable<string> CompilerOptions => _compilerOptions;

	public IEnumerable<string> CompilerIncludes => _compilerIncludes;

	public IEnumerable<string> CompilerDefines  => _compilerDefines;

	public IEnumerable<string> LinkerOptions => _linkerOptions;

	public IEnumerable<string> LinkerLibraries => _linkerLibraries;

	public IEnumerable<string> LinkerObjects => _linkerObjects;
}
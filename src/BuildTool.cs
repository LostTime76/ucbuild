using System.Diagnostics;
using CeetemSoft.Io;

using CeetemSoft.Processes;

namespace UcBuild;

/// <summary>
/// Provides the base class for all toolchain build tools
/// </summary>
/// <typeparam name="T">
/// The type of the work unit
/// </typeparam>
public abstract class BuildTool<T> where T: WorkUnit
{
	/// <summary>
	/// Gets the default format for generating the command line arguments to invoke the executable
	/// using a response file
	/// </summary>
	public const string DefaultResponseFormat = "@{0}";

	/// <summary>
	/// Initializes the build tool
	/// </summary>
	/// <param name="directory">
	/// The directory containing the executable
	/// </param>
	/// <param name="executable">
	/// The filename of the executable
	/// </param>
	protected BuildTool(string? directory, string? executable)
	{
		Executable = GetExecutable(directory, executable);
	}

	/// <summary>
	/// Invokes the tool
	/// </summary>
	/// <param name="workUnit">
	/// The work unit to invoke
	/// </param>
	/// <returns>
	/// True if the operation was successful, false otherwise
	/// </returns>
	public bool Invoke(T workUnit)
	{
		var destination = Path.StripExtension(workUnit.PrimaryOutput);

		// Make sure the output directory exists
		Directory.CreateIfMissing(Path.GetDirectoryName(destination));

		// Resolve the command line arguments
		workUnit.Arguments = ResolveArguments(workUnit, destination);

		// Invoke the tool
		return (workUnit.Result = Process.Exec(Executable, workUnit.Arguments)).Value.Success;
	}

	/// <summary>
	/// Gets the command line arguments passed to the executable
	/// </summary>
	/// <param name="workUnit">
	/// The work unit being invoked
	/// </param>
	/// <returns>
	/// The command line arguments
	/// </returns>
	protected abstract string[] GetCommandLineArguments(T workUnit);

	/// <summary>
	/// Gets the command line arguments used to invoke the executable using a response file
	/// </summary>
	/// <param name="filepath">
	/// the filepath of the response file to invoke the executable with
	/// </param>
	/// <returns>
	/// The command line arguments
	/// </returns>
	protected string[] GetResponseCommandLineArguments(string filepath) =>
		[string.Format(DefaultResponseFormat, filepath)];

	/// <summary>
	/// Produces a new enumerable containing all of the original options with a format string
	/// applied to them
	/// </summary>
	/// <param name="format">
	/// The format to apply to each option within <paramref name="options"/>. The format must
	/// contain a single placeholder.
	/// </param>
	/// <param name="options">
	/// The options to format
	/// </param>
	/// <returns>
	/// A new enumerable containing the formatted options
	/// </returns>
	protected IEnumerable<string> FormatOptions(string format, IEnumerable<string> options) =>
		options.Select(option => string.Format(format, option));

	private string ResolveArguments(T workUnit, string destination)
	{
		string arguments = Sclex.Join(GetCommandLineArguments(workUnit));

		if (arguments.Length < ProcessExtensions.MaxCommandLineLength)
		{
			return arguments;
		}

		var filepath = destination + ResponseFileOptions.FileExt;

		// Create a response file
		ResponseFileOptions.Create(filepath, arguments);

		// Adjust the arguments
		return Sclex.Join(GetResponseCommandLineArguments(filepath));
	}

	private string GetExecutable(string? directory, string? executable)
	{
		executable = string.IsNullOrEmpty(executable) ? DefaultExecutable : executable;

		return string.IsNullOrEmpty(directory) ? executable : Path.Combine(directory, executable);
	}

	/// <summary>
	/// Gets the filename of the default executable
	/// </summary>
	public abstract string DefaultExecutable { get; }

	/// <summary>
	/// Gets the response file options to use to invoke the executable if the length of the
	/// command line arguments passed to the executable exceed
	/// <see cref="ProcessExtensions.MaxCommandLineLength"/>
	/// </summary>
	public virtual ResponseFileOptions ResponseFileOptions => ResponseFileOptions.Default;

	/// <summary>
	/// Gets the filepath of the executable
	/// </summary>
	public string Executable { get; private init; }
}
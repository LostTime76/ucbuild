namespace UcBuild.Toolchains;

/// <summary>
/// Provides the base class for all toolchain build tools
/// </summary>
public abstract class BuildTool
{
	/// <summary>
	/// Initializes the tool
	/// </summary>
	/// <param name="directory">
	/// The directory containing the executable
	/// </param>
	/// <param name="executable">
	/// The filename of the executable
	/// </param>
	/// <exception cref="ArgumentException">
	/// Thrown if <paramref name="executable"/> is null or empty
	/// </exception>
	protected BuildTool(string? directory, string executable)
	{
		ArgumentException.ThrowIfNullOrEmpty(executable, nameof(executable));

		Executable = GetExecutable(directory, executable);
	}

	private static string GetExecutable(string? directory, string executable) =>
		string.IsNullOrEmpty(directory) ? executable : Path.Combine(directory, executable);

	/// <summary>
	/// Gets the filepath of the executable
	/// </summary>
	public string Executable { get; private init; }
}
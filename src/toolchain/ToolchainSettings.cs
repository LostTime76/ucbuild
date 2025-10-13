namespace UcBuild.Toolchain;

/// <summary>
/// Provides a means to configure some settings of a toolchain
/// </summary>
public readonly struct ToolchainSettings
{
	/// <summary>
	/// Gets or initializes the directory containing the toolchain executables
	/// </summary>
	public string? ExecutableDirectory { get; init; }

	/// <summary>
	/// Gets or initializes the filename of the compiler executable
	/// </summary>
	public string? CompilerExecutable { get; init; }

	/// <summary>
	/// Gets or initializes the filename of the linker executable
	/// </summary>
	public string? LinkerExecutable { get; init; }
}
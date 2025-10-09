namespace UcBuild.Toolchains;

/// <summary>
/// Contains some settings for creation of a toolchain
/// </summary>
public readonly struct ToolchainSettings
{
	/// <summary>
	/// Gets or initializes the name of the toolchain
	/// </summary>
	public string? Name { get; init; }

	/// <summary>
	/// Gets or initializes the directory containing the toolchain executables
	/// </summary>
	public string? Directory { get; init; }

	/// <summary>
	/// Gets or initializes the filename of the compiler executable
	/// </summary>
	public string? CompilerExecutable { get; init; }

	/// <summary>
	/// Gets or initializes the filename of the linker executable
	/// </summary>
	public string? LinkerExecutable { get; init; }
}
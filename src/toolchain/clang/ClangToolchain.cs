namespace UcBuild.Toolchain.Clang;

/// <summary>
/// Implements the clang toolchain
/// </summary>
public sealed class ClangToolchain : IToolchain
{
	/// <summary>
	/// Creates a new toolchain
	/// </summary>
	/// <param name="settings">
	/// The toolchain settings
	/// </param>
	public ClangToolchain(ToolchainSettings settings)
	{
		Compiler = new ClangCompiler(settings.ExecutableDirectory, settings.CompilerExecutable);
	}

	/// <summary>
	/// Creates a new instance of the toolchain
	/// </summary>
	/// <param name="settings">
	/// The toolchain settings
	/// </param>
	/// <returns>
	/// A new instance of the toolchain
	/// </returns>
	public IToolchain CreateInstance(ToolchainSettings settings) =>	new ClangToolchain(settings);

	/// <summary>
	/// Gets the name of the clang toolchain
	/// </summary>
	public string Name => "clang";

	/// <summary>
	/// Gets the compiler for the toolchain
	/// </summary>
	public Compiler Compiler { get; private init; }
}
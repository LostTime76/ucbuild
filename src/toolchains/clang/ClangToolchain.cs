namespace UcBuild.Toolchains.Clang;

public sealed class ClangToolchain : IToolchain
{
	/// <summary>
	/// Gets the name by which the toolchain can be referenced
	/// </summary>
	public const string Name = "clang";

	/// <summary>
	/// Creates a new clang toolchain
	/// </summary>
	/// <param name="settings">
	/// The toolchain settings
	/// </param>
	public ClangToolchain(ToolchainSettings settings)
	{
		Compiler = new ClangCompiler(settings.Directory, settings.CompilerExecutable);
		Linker   = new ClangLinker(settings.Directory, settings.LinkerExecutable);
	}

	/// <summary>
	/// Gets the compiler
	/// </summary>
	public Compiler Compiler { get; private init; }

	/// <summary>
	/// Gets the linker
	/// </summary>
	public Linker Linker { get; private init; }
}
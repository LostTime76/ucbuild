namespace UcBuild.Toolchain.Clang;

/// <summary>
/// Implements the clang compiler
/// </summary>
public sealed class ClangCompiler : Compiler
{
	/// <summary>
	/// Gets the flag used to instruct the compiler to only compile the source file
	/// </summary>
	public const string CompileFlag = "-c";

	/// <summary>
	/// Gets the flag used to instruct the compiler to generate a header dependency file
	/// </summary>
	public const string DependsFlag = "-MMD";

	/// <summary>
	/// Gets the option used to specify the filepath of the header dependency file produced by the
	/// compiler
	/// </summary>
	public const string DependsOutputOption = "-MF";

	/// <summary>
	/// Gets the option used to specify the filepath of the object file produced by the compiler
	/// </summary>
	public const string OutputOption = "-o";

	/// <summary>
	/// Creates a new compiler
	/// </summary>
	/// <param name="directory">
	/// The directory containing the compiler executable
	/// </param>
	/// <param name="executable">
	/// The filename of the executable
	/// </param>
	public ClangCompiler(string? directory, string? executable) : base(directory, executable) { }

	/// <summary>
	/// Gets the command line arguments used to invoke the compiler
	/// </summary>
	/// <param name="workUnit">
	/// The work unit being invoked
	/// </param>
	/// <returns>
	/// The command line arguments
	/// </returns>
	protected override string[] GetCommandLineArguments(CompileWorkUnit workUnit) => [
		CompileFlag, workUnit.SourceFilepath,
		OutputOption, workUnit.ObjectFilepath,
		DependsFlag, DependsOutputOption, workUnit.DependsFilepath,
		..workUnit.Target.CompilerOptions,
		..FormatIncludes(workUnit.Target.CompilerIncludes),
		..FormatDefines(workUnit.Target.ComilerDefines)
	];

	/// <summary>
	/// Gets a value that indicates if the compiler supports generation of listing files
	/// </summary>
	public override bool SupportsListings => false;

	/// <summary>
	/// Gets the default compiler executable
	/// </summary>
	public override string DefaultExecutable => "clang";
}
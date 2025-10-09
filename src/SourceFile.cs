using CeetemSoft.Processes;

namespace UcBuild;

/// <summary>
/// Provides a means to access source files within a build
/// </summary>
public sealed class SourceFile
{
	/// <summary>
	/// Creates a new source file
	/// </summary>
	/// <param name="filepath">
	/// The filepath of the source file
	/// </param>
	/// <param name="exclude">
	/// True to exclude the source file from the build, false otherwise
	/// </param>
	public SourceFile(string filepath, bool exclude = false)
	{
		Filepath = filepath;
		Exclude  = exclude;
	}

	/// <summary>
	/// Gets or a sets a value that indicates if the source file should be included within the
	/// build or not
	/// </summary>
	public bool Exclude { get; set; }

	/// <summary>
	/// Gets the filepath of the source file
	/// </summary>
	public string Filepath { get; private init; }

	/// <summary>
	/// Gets or sets the filepath of the object file produced by the compiler
	/// </summary>
	public string? ObjectFilepath { get; set; }

	/// <summary>
	/// Gets or sets the filepath of the header dependency file produced by the compiler
	/// </summary>
	public string? DependsFilepath { get; set; }

	/// <summary>
	/// Gets or sets the filepath of the listing file produced by the compiler
	/// </summary>
	public string? ListingFilepath { get; set; }

	/// <summary>
	/// Gets or sets the filepath of the response file used to pass arguments to the compiler
	/// </summary>
	public string? ResponseFilepath { get; set; }

	/// <summary>
	/// Gets or sets the result of compiling the source file
	/// </summary>
	public ProcessResult? CompileResult { get; set; }
}
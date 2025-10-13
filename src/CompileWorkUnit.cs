namespace UcBuild;

/// <summary>
/// Describes the inputs and outputs of a compiler process invocation
/// </summary>
public sealed class CompileWorkUnit : WorkUnit
{
	/// <summary>
	/// Gets the filepath of the object file produced by the compiler
	/// </summary>
	public override string PrimaryOutput => ObjectFilepath;

	/// <summary>
	/// Gets or initializes the filepath of the source file being compiled
	/// </summary>
	public required string SourceFilepath { get; init; }

	/// <summary>
	/// Gets or initializes the filepath of the object file produced by the compiler
	/// </summary>
	public required string ObjectFilepath { get; init; }

	/// <summary>
	/// Gets or initializes the filepath of the header dependency file produced by the compiler
	/// </summary>
	public required string DependsFilepath { get; init; }

	/// <summary>
	/// Gets or initializes the filepath of the listing file produced by the compiler
	/// </summary>
	public string? ListingFilepath { get; init; }
}
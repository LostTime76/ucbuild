using CeetemSoft.Processes;

namespace UcBuild;

/// <summary>
/// Describes the inputs and outputs of a single process invocation
/// </summary>
public abstract class WorkUnit
{
	/// <summary>
	/// Gets the filepath of the primary output of the process invocation
	/// </summary>
	public abstract string PrimaryOutput { get; }

	/// <summary>
	/// Gets or sets the command line arguments passed to the process
	/// </summary>
	public string? Arguments { get; set; }

	/// <summary>
	/// Gets or sets the result of the process invocation
	/// </summary>
	public ProcessResult? Result { get; set; }

	/// <summary>
	/// Gets or intitializes the target that owns the work unit
	/// </summary>
	public required BuildTarget Target { get; init; }
}
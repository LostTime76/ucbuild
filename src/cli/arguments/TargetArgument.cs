using System.CommandLine;

namespace UcBuild.Cli;

public sealed class TargetArgument : Argument<IEnumerable<string>>
{
	private const string _name = "target";

	private const string _description =
		"Selects one or more targets for the operation";

	public TargetArgument() : base(_name)
	{
		Description = _description;
	}
}
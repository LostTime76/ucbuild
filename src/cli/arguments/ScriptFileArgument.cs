using System.CommandLine;

namespace UcBuild.Cli;

public sealed class ScriptFileArgument : Argument<IEnumerable<string>>
{
	private const string _name = "script";

	private const string _description =
		"Selects one or more script targets for the operation";

	public ScriptFileArgument() : base(_name)
	{
		Description = _description;
	}
}
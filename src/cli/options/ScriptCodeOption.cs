using System.CommandLine;

namespace UcBuild.Cli;

public sealed class ScriptCodeOption : Option<IEnumerable<string>>
{
	private const string _name = "--code";

	private const string _description =
		"Runs arbitrary script code before the main script is invoked.";

	public ScriptCodeOption() : base(_name)
	{
		Description = _description;
	}
}
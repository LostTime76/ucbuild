using System.CommandLine;

namespace UcBuild.Cli;

public sealed class CleanCommand : BuildCommand
{
	private const string _name = "clean";

	private const string _description =
		"Cleans the project";

	public CleanCommand() : base(_name, _description)
	{
		SetAction(Invoke);
	}

	private int Invoke(ParseResult result)
	{
		return 0;
	}
}
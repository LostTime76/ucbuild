using System.CommandLine;

namespace UcBuild.Cli;

public sealed class LinkCommand : ScriptCommand
{
	private const string _name = "link";

	private const string _description = "Compiles and links the project";

	public LinkCommand() : base(_name, _description)
	{
		SetAction(Invoke);
	}

	private int Invoke(ParseResult result)
	{
		return 0;
	}
}
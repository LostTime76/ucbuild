using System.CommandLine;

namespace UcBuild.Cli;

public sealed class CompileCommand : ScriptCommand
{
	private const string _name = "compile";

	private const string _description =
		"Compiles the source code for a project";

	public CompileCommand() : base(_name, _description)
	{
		SetAction(Invoke);
	}

	private int Invoke(ParseResult result)
	{
		return 0;
	}
}
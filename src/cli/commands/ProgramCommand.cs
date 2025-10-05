using System.CommandLine;

namespace UcBuild.Cli;

public sealed class ProgramCommand : RootCommand
{
	private const string _description =
		"Tool used to build simple c based microcontroller projects";

	public ProgramCommand() : base(_description)
	{
		Add(new InfoCommand());
		Add(new CleanCommand());
		Add(new VscConfigureCommand());
		Add(new CompileCommand());
		Add(new LinkCommand());
	}
}
using System.CommandLine;

namespace UcBuild.Cli;

public abstract class ScriptCommand : Command
{
	protected ScriptCommand(string name, string description) : base(name, description)
	{
		Add(ScriptFilepath);
		Add(ScriptFragments);
	}

	protected ScriptFileOption ScriptFilepath { get; } = new();

	protected ScriptCodeOption ScriptFragments { get; } = new();
}
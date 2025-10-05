namespace UcBuild.Cli;

public abstract class BuildCommand : ScriptCommand
{
	protected BuildCommand(string name, string description) : base(name, description)
	{
		Add(Targets);
	}

	protected TargetArgument Targets { get; } = new();
}
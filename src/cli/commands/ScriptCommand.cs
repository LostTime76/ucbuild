using System.CommandLine;

namespace UcBuild.Cli;

public abstract class ScriptCommand : Command
{
	protected ScriptCommand(string name, string description) : base(name, description)
	{
		Add(ScriptFilepaths);
		Add(ScriptFragments);
	}

	protected IEnumerable<BuildTarget> GetTargets(ParseResult result)
	{
		var filepaths = result.GetValue(ScriptFilepaths) ?? [];
		var fragments = result.GetValue(ScriptFragments) ?? [];

		return BuildTarget.EvaluateTargets(filepaths, fragments);
	}

	protected ScriptCodeOption ScriptFragments { get; } = new();

	protected ScriptFileArgument ScriptFilepaths { get; } = new();
}
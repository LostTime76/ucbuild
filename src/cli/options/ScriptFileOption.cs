using System.CommandLine;

namespace UcBuild.Cli;

public sealed class ScriptFileOption : Option<string>
{
	private const string _name = "--file";

	private const string _description =
		"Sets the filepath of the main script to invoke.";

	public ScriptFileOption() : base(_name)
	{
		Description = _description;
		Required    = true;
	}
}
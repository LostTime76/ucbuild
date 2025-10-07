using System.CommandLine;

namespace UcBuild.Cli;

public sealed class VscConfigureCommand : ScriptCommand
{
	private const string _name = "vsc-configure";

	private const string _description =
		"Generates c_cpp_properties.json for visual studio code";

	public VscConfigureCommand() : base(_name, _description)
	{
		Add(VscDirectory);
		SetAction(Invoke);
	}

	private int Invoke(ParseResult result)
	{
		// Resolve the targets
		var targets = GetTargets(result);

		// Write the properties file
		VscProperties.Generate(result.GetValue(VscDirectory), targets);

		return Program.Success;
	}

	private VscDirectoryOption VscDirectory { get; } = new();
}
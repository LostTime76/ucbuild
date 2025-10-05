using System.CommandLine;

namespace UcBuild.Cli;

public sealed class VscDirectoryOption : Option<string>
{
	private const string _name = "--vsc_dir";

	private const string _description =
		"Sets the directory to generate c_cpp_properties.json within.";
		
	public VscDirectoryOption() : base("--vsc_dir")
	{
		Description = _description;
	}
}
using CeetemSoft.Attributes;
using System.CommandLine;
using System.Reflection;

namespace UcBuild.Cli;

public sealed class InfoCommand : Command
{
	private const string _name = "info";
	
	private const string _description =
		"Shows the build information for the tool";

	public InfoCommand() : base(_name, _description)
	{
		SetAction(Invoke);
	}

	private int Invoke(ParseResult result)
	{
		var assembly = Program.Assembly;
		var version  = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>()!.Version;
		var commit   = assembly.GetCustomAttribute<GitCommitHashAttribute>()!.CommitHash;
		var author   = assembly.GetCustomAttribute<AuthorAttribute>()!.Author;
		var time     = assembly.GetCustomAttribute<TimeAttribute>()!.Time;
		var format   = Resources.GetText(Resources.InfoText);

		Console.WriteLine(string.Format(format, version, commit, author, time));
		return Program.Success;
	}
}
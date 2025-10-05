using System.Reflection;
using UcBuild.Cli;

namespace UcBuild;

public static partial class Program
{
	public const int Success = 0;

	public static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
	
	public static int Main(string[] args)
	{
		return new ProgramCommand().Parse(args).Invoke();
	}
}
using System.Reflection;
using UcBuild.Runtime;

namespace UcBuild;

/// <summary>
/// Implements the main class of the program
/// </summary>
public static partial class Program
{
	/// <summary>
	/// Indicates the program exited successfully
	/// </summary>
	public const int Success = 0;

	/// <summary>
	/// Gets the executing assembly
	/// </summary>
	public static readonly Assembly Assembly = Assembly.GetExecutingAssembly();

	/// <summary>
	/// The main entry point of the program
	/// </summary>
	/// <param name="args">
	/// The command line arguments passed to the program
	/// </param>
	/// <returns>
	/// The exit code of the program
	/// </returns>
	public static int Main(string[] args)
	{
		LuaRuntime r = new(@"C:\Users\Colton\Desktop\Files\projects\ucbuild\cproj\build\project.lua");
		var t = r.ConfigureTarget("debug");

		return 0;
	}
}
using CeetemSoft.Io;
using System.Reflection;
using UcBuild.Toolchains;

namespace UcBuild;

public static partial class Program
{
	public const int Success = 0;

	public static readonly Assembly Assembly = Assembly.GetExecutingAssembly();

	public static int Main(string[] args)
	{
		var toolchain = IToolchain.Create(default);
		var compiler = toolchain.Compiler;
		var settings = new CompilerSettings();
		var source   = new SourceFile(Path.GetFullPath(Path.Combine(Path.GetSourceDirectory(), "../cproj/src/app/main.c")));

		bool r = compiler.ResolveOutputs(source, Path.GetSourceDirectory(), settings, new());

		r = compiler.Compile(source, settings);

		return 0;
	}
}
using System.Text;

namespace UcBuild.Toolchains.Clang;

/// <summary>
/// Implements the clang compiler
/// </summary>
public sealed class ClangCompiler : Compiler
{
	/// <summary>
	/// Gets the default name of the compiler executable
	/// </summary>
	public const string DefaultExecutable = "clang";

	/// <summary>
	/// Creates a new compiler
	/// </summary>
	/// <param name="directory">
	/// The directory containing the executable
	/// </param>
	/// <param name="executable">
	/// The filename of the executable
	/// </param>
	/// <exception cref="ArgumentException">
	/// Thrown if <paramref name="executable"/> is null or empty
	/// </exception>
	public ClangCompiler(string? directory, string? executable)
		: base(directory, executable ?? DefaultExecutable) { }
}
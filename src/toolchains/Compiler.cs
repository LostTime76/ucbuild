using CeetemSoft.Io;
using CeetemSoft.Processes;

namespace UcBuild.Toolchains;

/// <summary>
/// Provides the base class for a compiler within a toolchain
/// </summary>
public abstract class Compiler : BuildTool
{
	/// <summary>
	/// Gets the default file extension for object files produced by the compiler
	/// </summary>
	public const string DefaultObjectFileExt = ".o";

	/// <summary>
	/// Gets the default file extension for header dependency files produced by the compiler
	/// </summary>
	public const string DefaultDependsFileExt = ".d";

	/// <summary>
	/// Gets the default file extension for listing files produced by the compiler
	/// </summary>
	public const string DefaultListingFileExt = ".lst";

	/// <summary>
	/// Initializes the compiler
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
	protected Compiler(string? directory, string executable) : base(directory, executable) { }

	/// <summary>
	/// Resolves the filepaths of all the outputs produced by compiling a source file and
	/// determines if the source file must be compiled
	/// </summary>
	/// <param name="source">
	/// The source file to resolve the outputs of
	/// </param>
	/// <param name="outputDirectory"></param>
	/// <param name="settings"></param>
	/// <param name="depends"></param>
	/// <returns></returns>
	public bool ResolveOutputs(
		SourceFile source,
		string outputDirectory,
		CompilerSettings settings,
		DependsTable depends)
	{
		var filepath    = source.Filepath;
		var filename    = Path.GetFileNameWithoutExtension(filepath);
		var destination = Path.Combine(outputDirectory, filename);
		var glisting    = settings.GenerateListing;
		var timestamp   = File.GetTimestamp(filepath);

		source.ObjectFilepath  = destination + ObjectFileExt;
		source.DependsFilepath = destination + DependsFileExt;
		source.ListingFilepath = glisting ? destination + ListingFileExt : null;

		// Check if the object file is outdated
		if (File.GetTimestamp(source.ObjectFilepath) < timestamp)
		{
			return true;
		}

		// Check if the listing file is outdated
		else if (glisting && (File.GetTimestamp(source.ListingFilepath) < timestamp))
		{
			return true;
		}

		// Otherwise check if the header dependencies are outdated
		return depends.AreDependsNewer(source.DependsFilepath);
	}

	public bool Compile(SourceFile source, CompilerSettings settings)
	{
		return true;
	}

	/// <summary>
	/// Gets the file extension for object files produced by the compiler
	/// </summary>
	public virtual string ObjectFileExt => DefaultObjectFileExt;

	/// <summary>
	/// Gets the file extension for header dependency files produced by the compiler
	/// </summary>
	public virtual string DependsFileExt => DefaultDependsFileExt;

	/// <summary>
	/// Gets the file extension for listing files produced by the compiler
	/// </summary>
	public virtual string ListingFileExt => DefaultListingFileExt;
}
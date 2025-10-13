using CeetemSoft.Io;

namespace UcBuild;

/// <summary>
/// Provides the base class for a toolchain compiler
/// </summary>
public abstract class Compiler : BuildTool<CompileWorkUnit>
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
	/// Gets the default include format for the compiler
	/// </summary>
	public const string DefaultIncludeFormat = "-I{0}";

	/// <summary>
	/// Gets the default define format for the compiler
	/// </summary>
	public const string DefaultDefineFormat = "-D{0}";

	private readonly DependsTable _depends = new();

	/// <summary>
	/// Initializes the compiler
	/// </summary>
	/// <param name="directory">
	/// The directory containing the compiler executable
	/// </param>
	/// <param name="executable">
	/// The filename of the compiler executable
	/// </param>
	protected Compiler(string? directory, string? executable) : base(directory, executable) { }

	/// <summary>
	/// Determines whether a work unit is outdated and must be recompiled or not
	/// </summary>
	/// <param name="workUnit">
	/// The work unit to check
	/// </param>
	/// <returns>
	/// True if the work unit is outdated and needs to be recompiled, false otherwise
	/// </returns>
	public bool IsOutdated(CompileWorkUnit workUnit)
	{
		var stimestamp = File.GetTimestamp(workUnit.SourceFilepath);
		var otimestamp = File.GetTimestamp(workUnit.ObjectFilepath);
		var listing    = workUnit.ListingFilepath;

		// Check if the object file is outdated
		if (otimestamp < stimestamp)
		{
			return true;
		}

		// Check if the listing file is outdated
		else if ((listing != null) && (File.GetTimestamp(listing) < stimestamp))
		{
			return true;
		}

		// Lastly check if any of the header dependencies are outdated
		return _depends.AreDependsNewer(workUnit.DependsFilepath, otimestamp);
	}

	/// <summary>
	/// Creates a compile work unit for a source file
	/// </summary>
	/// <param name="target">
	/// The target that owns the work unit
	/// </param>
	/// <param name="sourceFilepath">
	/// The filepath of the source file to compile
	/// </param>
	/// <param name="outputDirectory">
	/// The directory where the generated output files will be stored
	/// </param>
	/// <returns>
	/// The work unit
	/// </returns>
	public CompileWorkUnit CreateWorkUnit(
		BuildTarget target, string sourceFilepath, string outputDirectory)
	{
		// Listings may not be supported for every toolchain for some reason... (clang, really?)
		bool glistings = target.GenerateListings && SupportsListings;

		// Use the current directory if no directory is specified
		outputDirectory = string.IsNullOrEmpty(outputDirectory) ?
			Directory.GetCurrentDirectory() : outputDirectory;

		// Use the filename as a base for the output files
		var filename = Path.GetFileNameWithoutExtension(sourceFilepath);

		// Combine the directory and filename for the base filepath of the output files
		var destination = Path.Combine(outputDirectory, filename);

		return new()
		{
			Target          = target,
			SourceFilepath  = sourceFilepath,
			ObjectFilepath  = destination + ObjectFileExt,
			DependsFilepath = destination + DependsFileExt,
			ListingFilepath = glistings ? destination + ListingFileExt : null	
		};
	}

	/// <summary>
	/// Formats a list of include paths using <see cref="DefaultIncludeFormat"/>
	/// </summary>
	/// <param name="includes">
	/// The include paths to format
	/// </param>
	/// <returns>
	/// The formatted include paths
	/// </returns>
	protected IEnumerable<string> FormatIncludes(IEnumerable<string> includes) =>
		FormatOptions(DefaultIncludeFormat, includes);

	/// <summary>
	/// Formats a list of compiler defines using <see cref="DefaultDefineFormat"/>
	/// </summary>
	/// <param name="defines">
	/// The compiler defines to format
	/// </param>
	/// <returns>
	/// The formatted compiler defines
	/// </returns>
	protected IEnumerable<string> FormatDefines(IEnumerable<string> defines) =>
		FormatOptions(DefaultDefineFormat, defines);

	/// <summary>
	/// Gets a value that indicates if the compiler supports generation of listing files
	/// </summary>
	public virtual bool SupportsListings => true;

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
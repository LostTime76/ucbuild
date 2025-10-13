namespace UcBuild;

/// <summary>
/// Provides access to embedded resources within the assembly
/// </summary>
public static class Resources
{
	/// <summary>
	/// Gets the path to the embedded info text resource
	/// </summary>
	public const string InfoText = "ucbuild.resources.info.txt";

	/// <summary>
	/// Gets all of the text within of embedded text resource
	/// </summary>
	/// <param name="path">
	/// The path of the resource within the assembly
	/// </param>
	/// <returns></returns>
	public static string GetText(string path)
	{
		using var reader = GetStream(path);
		return reader.ReadToEnd();
	}

	private static StreamReader GetStream(string path) =>
		new (Program.Assembly.GetManifestResourceStream(path)!);
}
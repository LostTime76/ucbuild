namespace UcBuild;

public static class Resources
{
	public const string InfoText = "ucbuild.resources.info.txt";

	public static string GetText(string path)
	{
		using var reader = GetStream(path);
		return reader.ReadToEnd();
	}

	private static StreamReader GetStream(string path) =>
		new (Program.Assembly.GetManifestResourceStream(path)!);
}
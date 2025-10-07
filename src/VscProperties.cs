using CeetemSoft.Io;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace UcBuild;

public sealed partial class VscProperties
{
	private const int _version = 4;

	private const string _filename = "c_cpp_properties.json";

	private static readonly JsonTypeInfo<VscProperties> JsonType =
		JsonContext.Default.VscProperties;

	public static void Generate(string? directory, IEnumerable<BuildTarget> targets)
	{
		VscProperties properties = new() {
			Version        = _version,
			Configurations = GetConfigurations(targets)
		};

		var json     = JsonSerializer.Serialize(properties, JsonType);
		var filepath = GetFilepath(directory);

		File.WriteAllTextIfDifferent(GetFilepath(directory), json);
	}

	private static string GetFilepath(string? directory)
	{
		directory = string.IsNullOrEmpty(directory) ? Directory.GetCurrentDirectory() : directory;

		return Path.Combine(directory, _filename);
	}

	private static Configuration[] GetConfigurations(IEnumerable<BuildTarget> targets)
	{
		var configurations = new List<Configuration>();

		foreach(var target in targets)
		{
			configurations.Add(GetConfiguration(target));
		}

		return [.. configurations];
	}

	private static Configuration GetConfiguration(BuildTarget target)
	{
		return new Configuration() {
			Name             = target.Name,
			IntellisenseMode = target.VscIntellisenseMode,
			Includes         = [..target.CompilerIncludes],
			Defines          = [..target.VscDefines, ..target.CompilerDefines]
		};
	}

	public sealed class Configuration
	{
		[JsonPropertyName("name")]
		public required string Name { get; init; }

		[JsonPropertyName("intellisenseMode")]
		public string? IntellisenseMode { get; init; }

		[JsonPropertyName("includePath")]
		public string[]? Includes { get; init; }

		[JsonPropertyName("defines")]
		public string[]? Defines { get; init; }
	}

	[JsonPropertyName("version")]
	public required int Version { get; init; }

	[JsonPropertyName("configurations")]
	public required Configuration[] Configurations { get; init; }

	[JsonSourceGenerationOptions(WriteIndented = true, RespectNullableAnnotations = true)]
	[JsonSerializable(typeof(VscProperties))]
	public partial class JsonContext : JsonSerializerContext { } 
}
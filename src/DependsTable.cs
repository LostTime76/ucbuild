using CeetemSoft.Io;
using CeetemSoft.Utils;
using System.Collections.Concurrent;
using System.Text;
using System.Text.RegularExpressions;

namespace UcBuild;

/// <summary>
/// Provides a means to track header dependencies for a c source file
/// </summary>
public sealed partial class DependsTable
{
	private static readonly Regex _headerFileExtPattern = GetHeaderFileExtPattern();

	private readonly ConcurrentDictionary<string, long> _timestamps = [];

	/// <summary>
	/// Determines if any of the header files specified within a header dependency file are newer
	/// than a given reference file. The header dependency file must be in makefile format.
	/// </summary>
	/// <param name="filepath">
	/// The filepath of the header dependency file to read
	/// </param>
	/// <param name="reference">
	/// The timestamp of a reference file to compare to
	/// </param>
	/// <returns>
	/// True if the header dependency file does not exist or any of the header files specified
	/// within the header dependency file are newer than the reference file, otherwise false
	/// </returns>
	/// <remarks>
	/// This function caches the timestamps of referenced header files for faster lookup.
	/// Additionally, the function is thread safe and can be invoked from multiple threads
	/// simultaneously.
	/// </remarks>
	public bool AreDependsNewer(string filepath, long reference)
	{
		if (File.GetTimestamp(filepath) < 0)
		{
			return true;
		}

		var depends = ReadDepends(filepath);
		var newer   = false;

		foreach(var header in depends)
		{
			long timestamp = _timestamps.GetOrAdd(header, File.GetTimestamp);

			newer |= (timestamp < 0) || (timestamp > reference);
		}

		return newer;
	}

	private static string[] ReadDepends(string dfilepath)
	{
		var reader  = new SpanReader<char>(File.ReadAllText(dfilepath));
		var depends = new List<string>();
		var text    = new StringBuilder();

		while (ReadFilepath(ref reader, text))
		{
			var filepath  = text.ToString();
			var extension = Path.GetExtension(filepath);

			if (!string.IsNullOrEmpty(extension) && _headerFileExtPattern.IsMatch(extension))
			{
				depends.Add(filepath);
			}
		}

		return [..depends];
	}

	private static bool ReadFilepath(ref SpanReader<char> reader, StringBuilder text)
	{
		text.Clear();

		if (!SkipWhitespace(ref reader))
		{
			return false;
		}

		AppendCharacters(ref reader, text);
		return true;
	}

	private static void AppendCharacters(ref SpanReader<char> reader, StringBuilder text)
	{
		text.Append(reader.Current);

		while (reader.Left > 0)
		{
			char value;

			switch(value = reader.Read())
			{
				case ' ':
				case '\n':
				case '\r':
					return;
				case '\\':
					AppendEscape(ref reader, text);
					break;
				default:
					text.Append(value);
					break;
			}
		}
	}

	private static void AppendEscape(ref SpanReader<char> reader, StringBuilder text)
	{
		char value = reader.Read();

		switch(value)
		{
			case '\\':
			case ' ':
				break;
			default:
				text.Append('\\');
				break;
		}

		text.Append(value);
	}

	private static bool SkipWhitespace(ref SpanReader<char> reader)
	{
		while(reader.Left > 0)
		{
			switch(reader.Read())
			{
				case '\\':
				case '\n':
				case '\r':
				case '\t':
				case ' ':
					break;
				default:
					return true;
			}
		}

		return false;
	}

	[GeneratedRegex(".*(h|hh|hpp)")]
	private static partial Regex GetHeaderFileExtPattern();
}
namespace UcBuild;

public sealed class CompilerSettings
{
	public const int DefaultCStandard = 17;

	public bool GenerateListing { get; init; }

	public int CStandard { get; init; } = DefaultCStandard;

	public IEnumerable<string>? Options { get; init; }

	public IEnumerable<string>? Includes { get; init; }

	public IEnumerable<string>? Defines { get; init; }
}
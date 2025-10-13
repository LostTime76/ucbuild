using System.Diagnostics.CodeAnalysis;
using UcBuild.Toolchain.Clang;

namespace UcBuild.Toolchain;

/// <summary>
/// Provides a means to access all of the supported toolchains within the project
/// </summary>
public static class Toolchains
{
	/// <summary>
	/// Gets the default toolchain
	/// </summary>
	public static readonly IToolchain Default = new ClangToolchain(default);

	private static readonly IEnumerable<IToolchain> _all = [Default];

	private static readonly Dictionary<string, IToolchain> _table =
		_all.ToDictionary(toolchain => toolchain.Name, toolchain => toolchain);

	/// <summary>
	/// Tries to create a toolchain with the given name
	/// </summary>
	/// <param name="name">
	/// The name of the toolchain
	/// </param>
	/// <param name="settings">
	/// The toolchain settings
	/// </param>
	/// <param name="toolchain">
	/// The created toolchain if it exists, otherwise null
	/// </param>
	/// <returns>
	/// True if the toolchain exists and was created, false otherwise
	/// </returns>
	public static bool TryCreate(
		string name, ToolchainSettings settings, [NotNullWhen(true)] out IToolchain? toolchain)
	{
		if (!_table.TryGetValue(name, out var found))
		{
			toolchain = null;
			return false;
		}

		toolchain = found.CreateInstance(settings);
		return true;
	}

	/// <summary>
	/// Gets the names of all the supported toolchains
	/// </summary>
	public static HashSet<IToolchain> All => field ??= [.._all];
}
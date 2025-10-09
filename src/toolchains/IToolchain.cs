using UcBuild.Toolchains.Clang;

namespace UcBuild.Toolchains;

/// <summary>
/// Provides the interface for a build toolchain
/// </summary>
public interface IToolchain
{
	/// <summary>
	/// Gets the compiler for the toolchain
	/// </summary>
	public Compiler Compiler { get; }

	/// <summary>
	/// Creates a new toolchain
	/// </summary>
	/// <param name="settings">
	/// The toolchain settings
	/// </param>
	/// <returns>
	/// The toolchain
	/// </returns>
	public static IToolchain Create(ToolchainSettings settings)
	{
		switch(settings.Name)
		{
			default:
				return new ClangToolchain(settings);
		}
	}
}
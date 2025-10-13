namespace UcBuild.Toolchain;

/// <summary>
/// Provides the interface for all toolchains
/// </summary>
public interface IToolchain
{
	/// <summary>
	/// Creates a new instance of the toolchain
	/// </summary>
	/// <param name="settings">
	/// The toolchain settings
	/// </param>
	/// <returns>
	/// A new instance of the toolchain
	/// </returns>
	public IToolchain CreateInstance(ToolchainSettings settings);

	/// <summary>
	/// Gets the name of the toolchain
	/// </summary>
	public string Name { get; }

	/// <summary>
	/// Gets the compiler for the toolchain
	/// </summary>
	public Compiler Compiler { get; }
}
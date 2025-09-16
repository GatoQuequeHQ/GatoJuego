namespace GatoQueque.GatoJuego.Core.Assets;

internal sealed class AssetLoadException : Exception
{
	public AssetLoadException()
		: base("A problem occurred while loading the asset")
	{ }

	public AssetLoadException(String message)
		: base($"A problem occurred while loading the asset: {message}")
	{ }

	public AssetLoadException(String message, Exception innerException)
		: base($"A problem occurred while loading the asset: {message}", innerException)
	{ }
}

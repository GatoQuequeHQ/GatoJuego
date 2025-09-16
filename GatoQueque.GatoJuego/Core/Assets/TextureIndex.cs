using System.IO;

namespace GatoQueque.GatoJuego.Core.Assets;

internal sealed class TextureIndex
{
	internal Texture TowerPlaceholder { get; } = new(GenerateAssetPath("tower_placeholder.png"));

	private static String GenerateAssetPath(String assetFileName) =>
		Path.Combine(Directory.GetCurrentDirectory(), "assets", assetFileName);
}

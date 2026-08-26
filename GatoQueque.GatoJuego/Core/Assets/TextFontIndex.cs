using System.IO;

namespace GatoQueque.GatoJuego.Core.Assets;

internal sealed class TextFontIndex
{
	private static String GenerateAssetPath(String assetFileName) =>
		Path.Combine(Directory.GetCurrentDirectory(), "assets", "fonts", assetFileName);
}

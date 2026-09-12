using System.Collections.Frozen;
using System.IO;

namespace GatoQueque.GatoJuego.Core.Assets;

public sealed class FontIndex : IEnumerable<KeyValuePair<String, Font>>
{
	private readonly FrozenDictionary<String, Font> _fonts;

	public FontIndex(IEnumerable<String> fontFileNames)
		: this(fontFileNames.ToArray())
	{
	}

	public FontIndex(params ReadOnlySpan<String> fontFileNames)
	{
		var fonts = new Dictionary<String, Font>(fontFileNames.Length);
		foreach (var fontFileName in fontFileNames)
			fonts[fontFileName] = new Font(GenerateAssetPath(fontFileName));
		_fonts = fonts.ToFrozenDictionary();
	}

	public void LoadToVram()
	{
		foreach (var font in _fonts.Values)
			font.LoadToVram();
	}

	public void Unload()
	{
		foreach (var font in _fonts.Values)
			font.Unload();
	}

	public Font this[String fontFileName] => _fonts[fontFileName];

	IEnumerator<KeyValuePair<String, Font>> IEnumerable<KeyValuePair<String, Font>>.GetEnumerator() =>
		_fonts.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public FrozenDictionary<String, Font>.Enumerator GetEnumerator() => _fonts.GetEnumerator();

	private static String GenerateAssetPath(String assetFileName) =>
		Path.Combine(Directory.GetCurrentDirectory(), "assets", "fonts", assetFileName);
}

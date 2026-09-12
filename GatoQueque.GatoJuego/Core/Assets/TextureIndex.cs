using System.Collections.Frozen;
using System.IO;

namespace GatoQueque.GatoJuego.Core.Assets;

public sealed class TextureIndex : IEnumerable<KeyValuePair<String, Texture>>
{
	private readonly FrozenDictionary<String, Texture> _textures;

	public TextureIndex(IEnumerable<String> textureFileNames)
		: this(textureFileNames.ToArray())
	{
	}

	public TextureIndex(params ReadOnlySpan<String> textureFileNames)
	{
		var textures = new Dictionary<String, Texture>(textureFileNames.Length);
		foreach (var textureFileName in textureFileNames)
			textures[textureFileName] = new Texture(GenerateAssetPath(textureFileName));
		_textures = textures.ToFrozenDictionary();
	}

	public Texture this[String textureFileName] => _textures[textureFileName];

	IEnumerator<KeyValuePair<String, Texture>> IEnumerable<KeyValuePair<String, Texture>>.GetEnumerator() =>
		_textures.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public FrozenDictionary<String, Texture>.Enumerator GetEnumerator() => _textures.GetEnumerator();

	private static String GenerateAssetPath(String assetFileName) =>
		Path.Combine(Directory.GetCurrentDirectory(), "assets", "textures", assetFileName);
}

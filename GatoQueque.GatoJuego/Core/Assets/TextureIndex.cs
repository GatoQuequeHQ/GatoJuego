using System.Collections.Frozen;
using System.IO;

namespace GatoQueque.GatoJuego.Core.Assets;

internal sealed class TextureIndex : IEnumerable<KeyValuePair<String, Texture>>
{
	private readonly FrozenDictionary<String, Texture> _textures;

	internal TextureIndex(IEnumerable<String> textureFileNames)
		: this(textureFileNames.ToArray())
	{
	}

	internal TextureIndex(params ReadOnlySpan<String> textureFileNames)
	{
		var textures = new Dictionary<String, Texture>(textureFileNames.Length);
		foreach (var textureFileName in textureFileNames)
			textures[textureFileName] = new Texture(GenerateAssetPath(textureFileName));
		_textures = textures.ToFrozenDictionary();
	}

	internal void LoadToRam()
	{
		foreach (var texture in _textures.Values)
			texture.LoadToRam();
	}

	internal void LoadToVram()
	{
		foreach (var texture in _textures.Values)
			texture.LoadToVram();
	}

	internal void Unload()
	{
		foreach (var texture in _textures.Values)
			texture.Unload();
	}

	internal Texture this[String textureFileName] => _textures[textureFileName];

	IEnumerator<KeyValuePair<String, Texture>> IEnumerable<KeyValuePair<String, Texture>>.GetEnumerator() =>
		_textures.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	internal FrozenDictionary<String, Texture>.Enumerator GetEnumerator() => _textures.GetEnumerator();

	private static String GenerateAssetPath(String assetFileName) =>
		Path.Combine(Directory.GetCurrentDirectory(), "assets", "textures", assetFileName);
}

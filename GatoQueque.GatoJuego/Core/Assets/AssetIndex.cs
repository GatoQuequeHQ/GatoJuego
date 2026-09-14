using System.Collections.Frozen;
using System.Diagnostics.Contracts;
using System.IO;

namespace GatoQueque.GatoJuego.Core.Assets;

public sealed class AssetIndex<TAsset> : IEnumerable<KeyValuePair<String, TAsset>> where TAsset : Asset
{
	private readonly FrozenDictionary<String, TAsset> _assets;

	public AssetIndex(
		Func<String, TAsset> createAsset,
		String subfolderName,
		params ReadOnlySpan<String> assetFileNames)
	{
		var assets = new Dictionary<String, TAsset>(assetFileNames.Length);
		foreach (var assetFileName in assetFileNames)
			assets[assetFileName] = createAsset(GenerateAssetPath(subfolderName, assetFileName));
		_assets = assets.ToFrozenDictionary();
	}

	public TAsset this[String assetFileName] => _assets[assetFileName];

	IEnumerator<KeyValuePair<String, TAsset>> IEnumerable<KeyValuePair<String, TAsset>>.GetEnumerator() =>
		_assets.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public FrozenDictionary<String, TAsset>.Enumerator GetEnumerator() => _assets.GetEnumerator();

	[Pure]
	private static String GenerateAssetPath(String subfolderName, String assetFileName) =>
		Path.Combine(Directory.GetCurrentDirectory(), "assets", subfolderName, assetFileName);
}

using System.Collections.Frozen;
using System.IO;

namespace GatoQueque.GatoJuego.Core.Assets;

public sealed class SoundIndex : IEnumerable<KeyValuePair<String, Sound>>
{
	private readonly FrozenDictionary<String, Sound> _sounds;

	public SoundIndex(IEnumerable<String> soundFileNames)
		: this(soundFileNames.ToArray())
	{
	}

	public SoundIndex(params ReadOnlySpan<String> soundFileNames)
	{
		var sounds = new Dictionary<String, Sound>(soundFileNames.Length);
		foreach (var soundFileName in soundFileNames)
			sounds[soundFileName] = new FileSound(GenerateAssetPath(soundFileName));
		_sounds = sounds.ToFrozenDictionary();
	}

	public Sound this[String soundFileName] => _sounds[soundFileName];

	IEnumerator<KeyValuePair<String, Sound>> IEnumerable<KeyValuePair<String, Sound>>.GetEnumerator() =>
		_sounds.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public FrozenDictionary<String, Sound>.Enumerator GetEnumerator() => _sounds.GetEnumerator();

	private static String GenerateAssetPath(String assetFileName) =>
		Path.Combine(Directory.GetCurrentDirectory(), "assets", "sounds", assetFileName);
}

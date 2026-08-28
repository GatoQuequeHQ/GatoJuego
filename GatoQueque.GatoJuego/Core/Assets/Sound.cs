using Raylib_cs;
using System.Diagnostics.CodeAnalysis;

namespace GatoQueque.GatoJuego.Core.Assets;

internal sealed class Sound : IDisposable
{
	private readonly String _filePath;
	private Boolean _disposed;
	private Raylib_cs.Sound? _inRam;

	internal Sound(String filePath)
	{
		_filePath = filePath;
	}

	[MemberNotNullWhen(true, nameof(_inRam))]
	internal Boolean IsLoadedInRam => _inRam is not null;

	internal Int32 LastUsedTick { get; private set; }

	internal Raylib_cs.Sound Value
	{
		get
		{
			LastUsedTick = Environment.TickCount;
			LoadToRam();
			return _inRam.Value;
		}
	}

	public void Dispose()
	{
		if (_disposed)
			return;

		GC.SuppressFinalize(this);
		_disposed = true;
	}

	[MemberNotNull(nameof(_inRam))]
	internal void LoadToRam()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);

		if (IsLoadedInRam)
			return;

		var inRam = Raylib.LoadSound(_filePath);
		if (!Raylib.IsSoundValid(inRam))
			throw new AssetLoadException($"Failed to load to the CPU a sound from the file: {_filePath}");

		_inRam = inRam;
	}

	internal void Unload()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);

		if (!IsLoadedInRam)
			return;

		Raylib.UnloadSound(_inRam.Value);
		_inRam = null;
	}

	~Sound()
	{
		Dispose();
	}
}

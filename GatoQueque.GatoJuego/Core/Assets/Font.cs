using Raylib_cs;
using System.Diagnostics.CodeAnalysis;

namespace GatoQueque.GatoJuego.Core.Assets;

internal sealed class Font : IDisposable
{
	private readonly String _filePath;
	private Raylib_cs.Font? _inVram;
	private Boolean _disposed;

	internal Font(String filePath)
	{
		_filePath = filePath;
	}

	internal Int32 LastUsedTick { get; private set; }

	[MemberNotNullWhen(true, nameof(_inVram))]
	internal Boolean IsLoadedInVram => _inVram is not null;

	internal Raylib_cs.Font Value
	{
		get
		{
			LastUsedTick = Environment.TickCount;
			LoadToVram();
			return _inVram.Value;
		}
	}

	[MemberNotNull(nameof(_inVram))]
	internal void LoadToVram()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);

		if (IsLoadedInVram)
			return;

		var inVram = Raylib.LoadFont(_filePath);
		if (!Raylib.IsFontValid(inVram))
			throw new AssetLoadException($"Failed to load to the GPU a texture from the file: {_filePath}");

		_inVram = inVram;
	}

	internal void Unload()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);

		if (!IsLoadedInVram)
			return;

		Raylib.UnloadFont(_inVram.Value);
		_inVram = null;
	}

	public void Dispose()
	{
		if (_disposed)
			return;

		Unload();
		GC.SuppressFinalize(this);
		_disposed = true;
	}

	~Font()
	{
		Dispose();
	}
}

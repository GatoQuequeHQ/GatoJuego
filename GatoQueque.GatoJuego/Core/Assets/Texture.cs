using Raylib_cs;
using System.Diagnostics.CodeAnalysis;

namespace GatoQueque.GatoJuego.Core.Assets;

internal sealed class Texture : IDisposable
{
	private readonly String _filePath;
	private Image _inRam;
	private Texture2D _inVram;
	private Boolean _disposed;

	internal Texture(String filePath)
	{
		_filePath = filePath;
	}

	internal Int32 LastUsedTick { get; private set; }

	internal Boolean IsLoaded => IsLoadedInRam || IsLoadedInVram;

	internal Boolean IsLoadedInRam { get; private set; }

	internal Boolean IsLoadedInVram { get; private set; }

	internal ref Image Image
	{
		get
		{
			ObjectDisposedException.ThrowIf(_disposed, this);
			LastUsedTick = Environment.TickCount;
			LoadToRam();
			return ref _inRam;
		}
	}

	internal ref Texture2D Value
	{
		get
		{
			ObjectDisposedException.ThrowIf(_disposed, this);
			LastUsedTick = Environment.TickCount;
			LoadToVram();
			return ref _inVram;
		}
	}

	[MemberNotNull(nameof(_inRam))]
	internal void LoadToRam()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);

		if (IsLoadedInRam)
			return;

		Image inRam;
		if (IsLoadedInVram)
		{
			inRam = Raylib.LoadImageFromTexture(_inVram);
			if (!Raylib.IsImageValid(inRam))
				throw new AssetLoadException($"Failed to load to the CPU a texture from the GPU: {_filePath}");
			_inRam = inRam;
			IsLoadedInRam = true;

			Raylib.UnloadTexture(_inVram);
			IsLoadedInVram = false;
			return;
		}

		inRam = Raylib.LoadImage(_filePath);
		if (!Raylib.IsImageValid(inRam))
			throw new AssetLoadException($"Failed to load to the CPU a texture from the file: {_filePath}");

		_inRam = inRam;
		IsLoadedInRam = true;
	}

	[MemberNotNull(nameof(_inVram))]
	internal void LoadToVram()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);

		if (IsLoadedInVram)
			return;

		Texture2D inVram;
		if (IsLoadedInRam)
		{
			inVram = Raylib.LoadTextureFromImage(_inRam);
			if (!Raylib.IsTextureValid(inVram))
				throw new AssetLoadException($"Failed to load to the GPU a texture from the CPU: {_filePath}");
			_inVram = inVram;
			IsLoadedInVram = true;

			Raylib.UnloadImage(_inRam);
			IsLoadedInRam = false;
			return;
		}

		inVram = Raylib.LoadTexture(_filePath);
		if (!Raylib.IsTextureValid(inVram))
			throw new AssetLoadException($"Failed to load to the GPU a texture from the file: {_filePath}");

		_inVram = inVram;
		IsLoadedInVram = true;
	}

	internal void Unload()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);

		if (IsLoadedInVram)
		{
			Raylib.UnloadTexture(_inVram);
			IsLoadedInVram = false;
		}
		else if (IsLoadedInRam)
		{
			Raylib.UnloadImage(_inRam);
			IsLoadedInRam = false;
		}
	}

	public void Dispose()
	{
		if (_disposed)
			return;

		Unload();
		GC.SuppressFinalize(this);
		_disposed = true;
	}

	~Texture()
	{
		Dispose();
	}
}

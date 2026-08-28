using Raylib_cs;
using System.Diagnostics.CodeAnalysis;

namespace GatoQueque.GatoJuego.Core.Assets;

internal sealed class Texture : IDisposable
{
	private readonly String _filePath;
	private Image? _inRam;
	private Texture2D? _inVram;
	private Boolean _disposed;

	internal Texture(String filePath)
	{
		_filePath = filePath;
	}

	internal Int32 LastUsedTick { get; private set; }

	internal Boolean IsLoaded => _inRam is not null || _inVram is not null;

	[MemberNotNullWhen(true, nameof(_inRam))]
	internal Boolean IsLoadedInRam => _inRam is not null;

	[MemberNotNullWhen(true, nameof(_inVram))]
	internal Boolean IsLoadedInVram => _inVram is not null;

	internal Image Image
	{
		get
		{
			LastUsedTick = Environment.TickCount;
			LoadToRam();
			return _inRam.Value;
		}
	}

	internal Texture2D Value
	{
		get
		{
			LastUsedTick = Environment.TickCount;
			LoadToVram();
			return _inVram.Value;
		}
	}

	[MemberNotNull(nameof(_inRam))]
	internal void LoadToRam()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);

		if (IsLoadedInRam)
			return;

		if (IsLoadedInVram)
		{
			var inRam = Raylib.LoadImageFromTexture(_inVram.Value);
			if (!Raylib.IsImageValid(inRam))
				throw new AssetLoadException($"Failed to load to the CPU a texture from the GPU: {_filePath}");
			_inRam = inRam;

			Raylib.UnloadTexture(_inVram.Value);
			_inVram = null;
			return;
		}

		_inRam = Raylib.LoadImage(_filePath);
		if (!Raylib.IsImageValid(_inRam.Value))
			throw new AssetLoadException($"Failed to load to the CPU a texture from the file: {_filePath}");
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
			inVram = Raylib.LoadTextureFromImage(_inRam.Value);
			if (!Raylib.IsTextureValid(inVram))
				throw new AssetLoadException($"Failed to load to the GPU a texture from the CPU: {_filePath}");
			_inVram = inVram;

			Raylib.UnloadImage(_inRam.Value);
			_inRam = null;
			return;
		}

		inVram = Raylib.LoadTexture(_filePath);
		if (!Raylib.IsTextureValid(inVram))
			throw new AssetLoadException($"Failed to load to the GPU a texture from the file: {_filePath}");

		_inVram = inVram;
	}

	internal void Unload()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);

		if (IsLoadedInVram)
		{
			Raylib.UnloadTexture(_inVram.Value);
			_inVram = null;
		}

		if (IsLoadedInRam)
		{
			Raylib.UnloadImage(_inRam.Value);
			_inRam = null;
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

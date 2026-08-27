using Raylib_cs;
using System.Diagnostics.CodeAnalysis;

namespace GatoQueque.GatoJuego.Core.Assets;

internal sealed class Texture
{
	private readonly String _filePath;
	private Image? _inRam;
	private Texture2D? _inVram;

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
		if (IsLoadedInRam)
			return;

		if (IsLoadedInVram)
		{
			_inRam ??= Raylib.LoadImageFromTexture(_inVram.Value);
			if (!Raylib.IsImageValid(_inRam.Value))
				throw new AssetLoadException($"Failed to load to the CPU a texture from the GPU: {_filePath}");

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
		if (IsLoadedInVram)
			return;

		if (IsLoadedInRam)
		{
			_inVram = Raylib.LoadTextureFromImage(_inRam.Value);
			if (!Raylib.IsTextureValid(_inVram.Value))
				throw new AssetLoadException($"Failed to load to the GPU a texture from the CPU: {_filePath}");

			Raylib.UnloadImage(_inRam.Value);
			_inRam = null;
			return;
		}

		_inVram = Raylib.LoadTexture(_filePath);
		if (!Raylib.IsTextureValid(_inVram.Value))
			throw new AssetLoadException($"Failed to load to the GPU a texture from the file: {_filePath}");
	}

	internal void Unload()
	{
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
}

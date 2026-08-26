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
		if (_inRam is not null)
			return;

		if (_inVram is not null)
		{
			_inRam ??= Raylib.LoadImageFromTexture(_inVram.Value);
			Raylib.UnloadTexture(_inVram.Value);
			_inVram = null;
			return;
		}

		_inRam = Raylib.LoadImage(_filePath);
	}

	[MemberNotNull(nameof(_inVram))]
	internal void LoadToVram()
	{
		if (_inVram is not null)
			return;

		if (_inRam is not null)
		{
			_inVram = Raylib.LoadTextureFromImage(_inRam.Value);
			Raylib.UnloadImage(_inRam.Value);
			_inRam = null;
			return;
		}

		_inVram = Raylib.LoadTexture(_filePath);
	}

	internal void Unload()
	{
		if (_inVram is not null)
		{
			Raylib.UnloadTexture(_inVram.Value);
			_inVram = null;
		}

		if (_inRam is not null)
		{
			Raylib.UnloadImage(_inRam.Value);
			_inRam = null;
		}
	}
}

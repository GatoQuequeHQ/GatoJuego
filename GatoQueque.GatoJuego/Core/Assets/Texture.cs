using Raylib_cs;
using System.Diagnostics.CodeAnalysis;

namespace GatoQueque.GatoJuego.Core.Assets;

internal sealed class Texture
{
	private readonly String _fileName;
	private Image? _inRam;
	private Texture2D? _inVRam;

	internal Texture(String fileName)
	{
		_fileName = fileName;
	}

	internal Int32 LastUsedTick { get; private set; }

	internal Texture2D Value
	{
		get
		{
			LastUsedTick = Environment.TickCount;
			LoadToVRam();
			return _inVRam.Value;
		}
	}

	[MemberNotNull(nameof(_inRam))]
	internal void LoadToRam()
	{
		if (_inRam is not null)
			return;

		if (_inVRam is not null)
		{
			_inRam ??= Raylib.LoadImageFromTexture(_inVRam.Value);
			Raylib.UnloadTexture(_inVRam.Value);
			_inVRam = null;
			return;
		}

		_inRam = Raylib.LoadImage(_fileName);
	}

	[MemberNotNull(nameof(_inVRam))]
	internal void LoadToVRam()
	{
		if (_inVRam is not null)
			return;

		if (_inRam is not null)
		{
			_inVRam = Raylib.LoadTextureFromImage(_inRam.Value);
			Raylib.UnloadImage(_inRam.Value);
			_inRam = null;
			return;
		}

		_inVRam = Raylib.LoadTexture(_fileName);
	}

	internal void Unload()
	{
		if (_inVRam is not null)
		{
			Raylib.UnloadTexture(_inVRam.Value);
			_inVRam = null;
		}

		if (_inRam is not null)
		{
			Raylib.UnloadImage(_inRam.Value);
			_inRam = null;
		}
	}
}

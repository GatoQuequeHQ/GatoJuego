using Raylib_cs;
using System.Diagnostics.CodeAnalysis;

namespace GatoQueque.GatoJuego.Core.Assets;

internal sealed class FileFont : Font
{
	private readonly String _filePath;
	private Raylib_cs.Font? _inVram;

	internal FileFont(String filePath)
	{
		_filePath = filePath;
	}

	internal override Raylib_cs.Font Value
	{
		get
		{
			LoadToVram();
			return _inVram.Value;
		}
	}

	[MemberNotNull(nameof(_inVram))]
	internal void LoadToVram()
	{
		if (_inVram is not null)
			return;

		_inVram = Raylib.LoadFont(_filePath);
		if (!Raylib.IsFontValid(_inVram.Value))
			throw new AssetLoadException($"Failed to load to the GPU a texture from the file: {_filePath}");
	}

	internal void Unload()
	{
		if (_inVram is null)
			return;

		Raylib.UnloadFont(_inVram.Value);
		_inVram = null;
	}
}

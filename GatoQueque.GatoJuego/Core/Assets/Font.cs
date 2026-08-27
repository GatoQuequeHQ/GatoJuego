namespace GatoQueque.GatoJuego.Core.Assets;

internal abstract class Font
{
	internal abstract Raylib_cs.Font Value { get; }

	public static implicit operator Raylib_cs.Font(Font font) => font.Value;
}

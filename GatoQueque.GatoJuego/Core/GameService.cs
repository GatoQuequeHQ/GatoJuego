using Raylib_cs;

namespace GatoQueque.GatoJuego.Core;

internal sealed class GameService
{
	/// <summary>
	///     Inicia el juego y ejecuta el loop principal.
	/// </summary>
	public void Run()
	{
		Raylib.InitWindow(800, 600, "Gato Juego");
		Raylib.InitAudioDevice();
		Raylib.SetTargetFPS(60);
		Raylib.SetWindowMinSize(800, 600);

		while (!Raylib.WindowShouldClose())
		{
			Raylib.BeginDrawing();
			Raylib.EndDrawing();
		}
	}
}

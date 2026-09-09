using Raylib_cs;

namespace GatoQueque.GatoJuego.Core;

internal sealed class GameService : BackgroundService
{
	/// <summary>
	///     Inicia el juego y ejecuta el loop principal.
	/// </summary>
	/// <param name="stoppingToken"></param>
	protected override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		Raylib.InitWindow(800, 600, "Gato Juego");
		Raylib.InitAudioDevice();
		Raylib.SetTargetFPS(60);
		Raylib.SetWindowMinSize(800, 600);

		while (!Raylib.WindowShouldClose() && !stoppingToken.IsCancellationRequested)
		{
			Raylib.BeginDrawing();
			Raylib.EndDrawing();
		}

		return Task.CompletedTask;
	}
}

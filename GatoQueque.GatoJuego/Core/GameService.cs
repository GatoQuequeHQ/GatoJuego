namespace GatoQueque.GatoJuego.Core;

internal sealed class GameService : BackgroundService
{
	private const Int32 MaxFps = 60;
	private readonly TimeSpan _frameTime = TimeSpan.FromSeconds(1.0 / MaxFps);

	/// <summary>
	/// Inicia el juego y ejecuta el loop principal.
	/// </summary>
	/// <param name="stoppingToken"></param>
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			// TODO:
			await Task.Delay(_frameTime, stoppingToken);
		}
	}
}

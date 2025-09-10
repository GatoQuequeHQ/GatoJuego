using GatoQueque.GatoJuego.Core;
using GatoQueque.GatoJuego.Shared.Logging;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging
	.ClearProviders()
	.AddLogging();

builder.Services.AddHostedService<GameService>();

var host = builder.Build();
host.Run();


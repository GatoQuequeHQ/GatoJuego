using GatoQueque.GatoJuego.Core;
using GatoQueque.GatoJuego.Core.Assets;
using GatoQueque.GatoJuego.Core.Content;
using GatoQueque.GatoJuego.Shared.Logging;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging
	.ClearProviders()
	.AddLogging();


builder.Services.AddLocalization(options =>
{
	options.ResourcesPath = "Resources";
});
builder.Services.AddAssets();
builder.Services.AddContent();
builder.Services.AddHostedService<GameService>();

var host = builder.Build();
host.Run();

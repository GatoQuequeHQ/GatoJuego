using GatoQueque.GatoJuego.Core;
using GatoQueque.GatoJuego.Core.Assets;
using GatoQueque.GatoJuego.Core.Content;
using GatoQueque.GatoJuego.Shared.Logging;

var services = new ServiceCollection();

services.AddLogging(builder =>
{
	builder.ClearProviders();
	builder.AddLogging();
});
services.AddLocalization(options =>
{
	options.ResourcesPath = "Resources";
});
services.AddAssets();
services.AddContent();
services.AddSingleton<GameService>();

var serviceProvider = services.BuildServiceProvider();
var game = serviceProvider.GetRequiredService<GameService>();
game.Run();

using System.IO;

namespace GatoQueque.GatoJuego.Core.Assets;

internal static class DependencyInjection
{
	extension(IServiceCollection services)
	{
		internal IServiceCollection AddAssets()
		{
			services.AddSingleton<TextureIndex>(_ =>
			{
				var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "assets", "textures");
				var filePaths = Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories);
				return new TextureIndex(filePaths);
			});
			services.AddSingleton<FontIndex>(_ =>
			{
				var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "assets", "fonts");
				var filePaths = Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories);
				return new FontIndex(filePaths);
			});
			services.AddSingleton<SoundIndex>(_ =>
			{
				var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "assets", "sounds");
				var filePaths = Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories);
				return new SoundIndex(filePaths);
			});
			return services;
		}
	}
}

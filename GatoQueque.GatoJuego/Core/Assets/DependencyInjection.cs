using System.IO;

namespace GatoQueque.GatoJuego.Core.Assets;

internal static class DependencyInjection
{
	extension(IServiceCollection services)
	{
		internal IServiceCollection AddAssets()
		{
			services.AddSingleton<AssetIndex<Texture>>(_ =>
			{
				var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "assets", "textures");
				var filePaths = Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories);
				return new AssetIndex<Texture>(path => new Texture(path), "textures", filePaths);
			});
			services.AddSingleton<AssetIndex<Font>>(_ =>
			{
				var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "assets", "fonts");
				var filePaths = Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories);
				return new AssetIndex<Font>(path => new Font(path), "fonts", filePaths);
			});
			services.AddSingleton<AssetIndex<Sound>>(_ =>
			{
				var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "assets", "sounds");
				var filePaths = Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories);
				return new AssetIndex<Sound>(path => new FileSound(path), "sounds", filePaths);
			});
			return services;
		}
	}
}

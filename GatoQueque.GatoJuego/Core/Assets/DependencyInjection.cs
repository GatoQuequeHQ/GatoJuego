using System.IO;

namespace GatoQueque.GatoJuego.Core.Assets;

internal static class DependencyInjection
{
	extension(IServiceCollection services)
	{
		internal IServiceCollection AddAssets()
		{
			services.AddSingleton<TextureIndex>();
			services.AddSingleton<FontIndex>(_ =>
			{
				var texturesPath = Path.Combine(Directory.GetCurrentDirectory(), "assets", "fonts");
				var fileNames = Directory.GetFiles(texturesPath, "*.*", SearchOption.AllDirectories);
				return new FontIndex(fileNames);
			});
			return services;
		}
	}
}

namespace GatoQueque.GatoJuego.Core.Assets;

internal static class DependencyInjection
{
	extension(IServiceCollection services)
	{
		internal IServiceCollection AddAssets()
		{
			services.AddSingleton<TextureIndex>();
			return services;
		}
	}
}

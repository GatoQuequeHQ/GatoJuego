namespace GatoQueque.GatoJuego.Core.Content;

internal static class DependencyInjection
{
	extension(IServiceCollection services)
	{
		internal IServiceCollection AddContent()
		{
			services.AddSingleton<BiomeDescriptorIndex>()
				.AddSingleton<TowerDescriptorIndex>()
				.AddSingleton<TowerCategoryIndex>();
			return services;
		}
	}
}

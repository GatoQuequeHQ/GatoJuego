namespace GatoQueque.GatoJuego.Shared.Logging;

internal static class DependencyInjection
{
	internal static IServiceCollection AddLogs(this IServiceCollection services)
	{
		services.TryAddSingleton<ILoggerProvider, DefaultLoggerProvider>();
		return services;
	}
}

namespace GatoQueque.GatoJuego.Shared.Logging;

internal static class DependencyInjection
{
	internal static ILoggingBuilder AddLogging(this ILoggingBuilder builder)
	{
		builder.Services.TryAddSingleton<ILoggerProvider, DefaultLoggerProvider>();
		return builder;
	}
}

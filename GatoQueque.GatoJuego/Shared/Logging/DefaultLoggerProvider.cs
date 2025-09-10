namespace GatoQueque.GatoJuego.Shared.Logging;

internal sealed class DefaultLoggerProvider : ILoggerProvider
{
	// There is no need to use a concurrent dictionary, extra instances created
	// simultaneously by the CreateLogger method will be automatically collected by the GC.
	private readonly Dictionary<String, DefaultLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);

	public ILogger CreateLogger(String categoryName)
	{
		if (_loggers.TryGetValue(categoryName, out var logger))
			return logger;

		logger = new DefaultLogger(categoryName);
		_loggers.TryAdd(categoryName, logger);
		return logger;
	}

	public void Dispose()
	{
		_loggers.Clear();
	}
}

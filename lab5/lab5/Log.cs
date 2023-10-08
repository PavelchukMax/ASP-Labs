namespace lab5
{
    public class Flog : ILogger, IDisposable
    {
        string fpath;
        static object _lock = new object();
        public Flog(string path)
        {
            this.fpath = path;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }
        public void Dispose() { }
        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == LogLevel.Error;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            lock (_lock)
            {
                if (IsEnabled(logLevel))
                {
                    File.AppendAllText(fpath, formatter(state, exception) + Environment.NewLine);
                }
            }
        }
    }
    public static class FileLoggerextensions
    {
        public static ILoggingBuilder AddFile(this ILoggingBuilder builder, string filePath)

        {

            builder.AddProvider(new Provider(filePath));

            return builder;

        }

    }
}


namespace lab5
{
    public class Provider: ILoggerProvider
    {
        string path;

        public Provider(string path)
        {
            this.path = path;
        }
        public ILogger CreateLogger(string name)
        {
            return new Flog(path);
        }

        public void Dispose() { }
    }
}

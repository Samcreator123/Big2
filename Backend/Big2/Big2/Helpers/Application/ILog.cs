namespace Big2.Helpers.Application
{
    public interface ILog
    {
        void LogTrace(string message);
        void LogTrace<T>(string message, T obj);
        void LogDebug(string message);
        void LogDebug<T>(string message, T obj);
        void LogInfo(string message);
        void LogInfo<T>(string message, T obj);
        void LogWarning(string message);
        void LogWarning<T>(string message, T obj);
        void LogWarning<T>(string message, T obj, Exception ex);
        void LogError<T>(string message, T obj, Exception ex);
        void LogCritical<T>(string message, T obj, Exception ex);

    }
}

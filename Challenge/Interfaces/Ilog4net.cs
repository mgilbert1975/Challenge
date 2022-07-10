namespace Challenge.Interfaces
{
    public interface Ilog4net
    {
        void LogInfo(string msg);
        void LogDebug(string msg);
        void LogWarn(string msg);
        void LogError(string msg, Exception ex = null);
    }
}

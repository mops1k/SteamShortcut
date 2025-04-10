namespace Logger;

public interface ILogger
{
    public void Trace(object subject);
    public void Info(string format, params object?[] arg);
    public void Debug(string format, params object?[] arg);
    public void Error(string format, params object?[] arg);
    public void Fatal(string type, object? name, Exception e);
    public void Fatal(string type, Exception? e);
}

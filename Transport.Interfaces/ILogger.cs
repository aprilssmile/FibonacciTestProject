namespace Transport.Interfaces
{
    public interface ILogger
    {
        void Error<T>(T message);
        void Debug<T>(T message);
        void Trace<T>(T message);
        void Info<T>(T message);
    }
}
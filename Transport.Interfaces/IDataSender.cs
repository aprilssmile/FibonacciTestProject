using System.Threading.Tasks;

namespace Transport.Interfaces
{
    public interface IDataSender
    {
        Task SendResult<T>(T message) where T : class;
    }
}

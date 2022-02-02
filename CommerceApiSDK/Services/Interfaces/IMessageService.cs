using System.Threading.Tasks;
using CommerceApiSDK.Models;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IMessageService
    {
        Task<MessageDto> AddMessage(MessageDto message);
    }
}

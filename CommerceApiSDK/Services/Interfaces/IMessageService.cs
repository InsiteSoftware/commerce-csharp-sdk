namespace CommerceApiSDK.Services.Interfaces
{
    using System.Threading.Tasks;
    using CommerceApiSDK.Models;

    public interface IMessageService
    {
        Task<MessageDto> AddMessage(MessageDto message);
    }
}

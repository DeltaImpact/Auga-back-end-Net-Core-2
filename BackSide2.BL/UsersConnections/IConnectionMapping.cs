using System.Threading.Tasks;
using Auga.DAO.Entities;

namespace Auga.BL.UsersConnections
{
    public interface IConnectionMapping
    {
        Task Add(string connectionId);
        Task Remove(string connectionId);
        Task<ChatConnectedUser> GetConnectionIdByUserid(long userId);
        Task<bool> IsOnline(long userId);
    }
}
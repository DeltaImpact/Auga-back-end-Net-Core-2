using System.Collections.Generic;
using System.Threading.Tasks;
using Auga.DAO.Entities;

namespace Auga.BL.UsersConnections
{
    public interface IMatchmakingService
    {
        Task<List<GameWaitingUser>> GetWaitingRaffle(long itemId);
        Task<GameWaitingUser> JoinRaffle(long itemId);
        Task<GameWaitingUser> LeaveRaffle(long itemId);
    }
}
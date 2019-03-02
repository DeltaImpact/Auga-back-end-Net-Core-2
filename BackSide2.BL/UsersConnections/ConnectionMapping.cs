using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Auga.BL.Exceptions;
using Auga.DAO.Entities;
using Auga.DAO.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Auga.BL.UsersConnections
{
    public class ConnectionMapping : IConnectionMapping
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<ChatConnectedUser> _chatConnectedUsers;
        private readonly IRepository<GameWaitingUser> _gameWaitingUserRepository;

        public ConnectionMapping(IHttpContextAccessor httpContextAccessor,
            IRepository<ChatConnectedUser> chatConnectedUsers,
            IRepository<GameWaitingUser> gameWaitingUserRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _chatConnectedUsers = chatConnectedUsers;
            _gameWaitingUserRepository = gameWaitingUserRepository;
        }

        public async Task Add(string connectionId)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var connectionInDb = await (await _chatConnectedUsers.GetAllAsync(o => o.UserId == userId))
                .FirstOrDefaultAsync();
            if (connectionInDb != null) await _chatConnectedUsers.RemoveAsync(connectionInDb);
            //if (connectionInDb) await Remove(connectionId);
            var connection = new ChatConnectedUser
            {
                CreatedBy = userId,
                UserId = userId,
                ConnectionId = connectionId
            };
            await _chatConnectedUsers.InsertAsync(connection);
        }

        public async Task<ChatConnectedUser> GetConnectionIdByUserid(long userId)
        {
            var connectionInDb = await (await _chatConnectedUsers.GetAllAsync(o => o.UserId == userId))
                .FirstOrDefaultAsync();
            if (connectionInDb == null)
            {
                throw new ObjectNotFoundException("Connection not found.");
            }

            return connectionInDb;
        }

        public async Task Remove(string connectionId)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var connectionInDb =
                await (await _chatConnectedUsers.GetAllAsync(o => o.ConnectionId == connectionId))
                    .FirstOrDefaultAsync();
            if (connectionInDb == null)
            {
                throw new ObjectNotFoundException("Connection not found.");
            }

            if (connectionInDb.CreatedBy != userId)
            {
                throw new UnauthorizedAccessException("You have no permissions to delete this pin.");
            }

            await _gameWaitingUserRepository.RemoveManyAsync(
                (await (await _gameWaitingUserRepository.GetAllAsync(e => e.UserId == userId)).ToListAsync()));
            await _chatConnectedUsers.RemoveAsync(connectionInDb);
        }

        public async Task<bool> IsOnline(long userId)
        {
            var isOnline = (await (await _chatConnectedUsers.GetAllAsync(o => o.UserId == userId)).AnyAsync());
            return isOnline;
        }
    }
}
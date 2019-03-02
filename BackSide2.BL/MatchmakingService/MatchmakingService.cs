using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Auga.BL.Exceptions;
using Auga.DAO.Entities;
using Auga.DAO.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Auga.BL.UsersConnections
{
    public class MatchmakingService : IMatchmakingService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<ChatConnectedUser> _chatConnectedUsersRepository;
        private readonly IRepository<GameWaitingUser> _gameWaitingUserRepository;


        public MatchmakingService(IHttpContextAccessor httpContextAccessor,
            IRepository<ChatConnectedUser> chatConnectedUsersRepository,
            IRepository<GameWaitingUser> gameWaitingUserRepository
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _chatConnectedUsersRepository = chatConnectedUsersRepository;
            _gameWaitingUserRepository = gameWaitingUserRepository;
        }

        public async Task Add(string connectionId)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var connectionInDb = await (await _chatConnectedUsersRepository.GetAllAsync(o => o.UserId == userId))
                .FirstOrDefaultAsync();
            if (connectionInDb != null) await _chatConnectedUsersRepository.RemoveAsync(connectionInDb);
            //if (connectionInDb) await Remove(connectionId);
            var connection = new ChatConnectedUser
            {
                CreatedBy = userId,
                UserId = userId,
                ConnectionId = connectionId
            };
            await _chatConnectedUsersRepository.InsertAsync(connection);
        }

        public async Task Remove(string connectionId)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var connectionInDb =
                await (await _chatConnectedUsersRepository.GetAllAsync(o => o.ConnectionId == connectionId)).FirstOrDefaultAsync();
            if (connectionInDb == null)
            {
                throw new ObjectNotFoundException("Connection not found.");
            }

            if (connectionInDb.CreatedBy != userId)
            {
                throw new UnauthorizedAccessException("You have no permissions to delete this pin.");
            }

            await _chatConnectedUsersRepository.RemoveAsync(connectionInDb);
        }


        public async Task<List<GameWaitingUser>> GetWaitingRaffle(long itemId)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userNickname = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value);


            var item =
                await (await _gameWaitingUserRepository.GetAllAsync(d => d.ItemId == itemId)).ToListAsync();

            return item;
            //return new List<GameWaitingUser>();
        }

        public async Task<GameWaitingUser> JoinRaffle(long itemId)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userNickname = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value);

           var itemToAdd = new GameWaitingUser
            {
                UserId = userId,
                ItemId = itemId,
                Username = userNickname
            };
            var item = (await _gameWaitingUserRepository.InsertAsync(itemToAdd));
            return item;
        }

        public async Task<GameWaitingUser> LeaveRaffle(long itemId)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userNickname = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value);
            var itemInDb =
                await(await _gameWaitingUserRepository.GetAllAsync(d => d.ItemId == itemId && d.UserId == userId))
                    .FirstOrDefaultAsync();

            if (itemInDb == null)
            {
                throw new ObjectNotFoundException("Item not found.");
            }

            //var item = (await _itemService.RemoveAsync(userInDb)).ToBoardReturnDto();
            var item = (await _gameWaitingUserRepository.RemoveAsync(itemInDb));
            return item;
        }
        
    }
}
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
                await (await _gameWaitingUserRepository.GetAllAsync(d => d.ItemId == itemId && d.UserId == userId))
                    .FirstOrDefaultAsync();

            if (itemInDb == null)
            {
                throw new ObjectNotFoundException("Item not found.");
            }

            var item = (await _gameWaitingUserRepository.RemoveAsync(itemInDb));
            await _gameWaitingUserRepository.RemoveManyAsync(
                (await (await _gameWaitingUserRepository.GetAllAsync(e => e.UserId == userId)).ToListAsync()));
            return item;
        }
    }
}
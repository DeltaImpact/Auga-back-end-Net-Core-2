using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auga.BL.Models.ChatDto;
using Auga.BL.UsersConnections;
using Auga.DAO.Entities;
using Auga.DAO.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace Auga.Controllers
{
    [Route("games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IMatchmakingService _matchmakingService;
        private readonly IConnectionMapping _connectionMapping;
        private readonly IRepository<User> _personService;
        private readonly IRepository<ChatConnectedUser> _chatMessageRepository;
        private readonly IRepository<GameWaitingUser> _gameWaitingUser;


        public GamesController(IHttpContextAccessor httpContextAccessor, IHubContext<ChatHub> hubContext,
            IMatchmakingService matchmakingService, IConnectionMapping connectionMapping)
        {
            _httpContextAccessor = httpContextAccessor;
            _hubContext = hubContext;
            _matchmakingService = matchmakingService;
            _connectionMapping = connectionMapping;
        }


        /// <summary>
        /// Gets a list of users waiting to raffle the item
        /// </summary> 
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetGame([Required] int id)
        {
            try
            {
                var responsePayload = await _matchmakingService.GetWaitingRaffle(id);
                return Ok(responsePayload);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
            }

            //var identity = _httpContextAccessor.HttpContext.User.Identity;
            //var asd = _httpContextAccessor.HttpContext;

            //await _hubContext.Clients.All.SendAsync("SendAction", id);
        }

        /// <summary>
        /// Joins the raffle
        /// </summary> 
        [Authorize]
        [HttpPost("{id}/join")]
        public async Task<IActionResult> JoinGame([Required] int id)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var joinedUser = await _matchmakingService.JoinRaffle(id);

            //await _hubContext.Clients.Groups(id.ToString()).SendAsync("userJoined", joinedUser.ToString());
            await _hubContext.Clients.Groups(id.ToString()).SendAsync("userJoined", JsonConvert.SerializeObject(joinedUser));
            await _hubContext.Groups.AddToGroupAsync(
                (await _connectionMapping.GetConnectionIdByUserid(userId)).ConnectionId,
                id.ToString());

            //await _hubContext.Clients.All.SendAsync("moves", JsonConvert.SerializeObject(joinedUser));
            //var gameUsers = await _matchmakingService.GetWaitingRaffle(id);
            //var usersConnectionsIds = gameUsers
            //    .Select(async e => (await _connectionMapping.GetConnectionIdByUserid(e.UserId)).ConnectionId).ToList();

            //var asd = usersConnectionsIds.Select(async e => await e);
            //await usersConnectionsIds.Select(e => (await _hubContext.Clients.Client(e)))
            return Ok(joinedUser);
        }

        /// <summary>
        /// Leaves the raffle
        /// </summary> 
        [Authorize]
        [HttpPost("{id}/leave")]
        public async Task<IActionResult> LeaveGame([Required] int id, MessageReceivedContext receivedContext)
        {
            var joinedUser = await _matchmakingService.LeaveRaffle(id);
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _hubContext.Groups.RemoveFromGroupAsync(
                (await _connectionMapping.GetConnectionIdByUserid(userId)).ConnectionId,
                id.ToString());
            await _hubContext.Clients.Groups(id.ToString()).SendAsync("userExited", JsonConvert.SerializeObject(joinedUser));

            return Ok(joinedUser);

            await _hubContext.Clients.All.SendAsync("SendAction", id);
        }


        //[Authorize]
        //[HttpPost("sendMessage")]
        //public async void SendMessage(
        //    GetPmDto message
        //)
        //{
        //    try
        //    {
        //        var userId = long.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //        var userId2 = message.SentTo;
        //        //var targetUsr = await (await _personService.GetAllAsync(d => d.Id == userId2)).FirstOrDefaultAsync();
        //        await _hubContext.Clients.All.SendAsync("MessageSend", userId, userId2, message);
        //        //await _hubContext.Clients.All.SendAsync("SendAction", userId, userId2, message);

        //        //var user = await _personService.GetByIdAsync(userId);
        //        //var sentToUser = await _personService.GetByIdAsync(message.SentTo);
        //        //var sentToUser1 = await _personService.GetByIdAsync(message.SentTo);
        //        //var sentToUser2 = await _personService.GetByIdAsync(message.SentTo);

        //        //var user = await (await _personService.GetAllAsync(d => d.Id == userId)).FirstOrDefaultAsync();

        //        //var userId = long.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //        //var callerUserTask = _personService.GetByIdAsync(userId);
        //        //var targetUserTask = _personService.GetByIdAsync(message.SentTo);

        //        //var userId2 = message.SentTo;
        //        //var user = await callerUserTask;
        //        //var sentToUser = await targetUserTask;
        //        //var user = await _personService.GetByIdAsync(userId);
        //        //var sentToUser = await _personService.GetByIdAsync(message.SentTo);

        //        //if (sentToUser == null)
        //        //    throw new ArgumentException("User with this id does not exist");

        //        //var newChatMessage = new ChatMessage
        //        //{
        //        //    CreatedBy = user.Id,
        //        //    MessageContent = message.Message,
        //        //    ReceivedBy = sentToUser
        //        //};
        //        //await _chatMessageRepository.InsertAsync(newChatMessage);
        //        //await _hubContext.Clients.User(sentToUser?.Id.ToString())
        //        //    .SendAsync("MessageReceived", user.UserName, message, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        BadRequest(new {ex.Message});
        //    }
        //}

        //[Authorize]
        //[HttpPut("startTyping")]
        //public async Task StartTyping()
        //{
        //    var userId = long.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //    var user = await _personService.GetByIdAsync(userId);
        //    if (user == null)
        //    {
        //        BadRequest("User not found.");
        //    }
        //    else
        //        await _hubContext.Clients.All.SendAsync("StartTyping", user.UserName);
        //}

        //[Authorize]
        //[HttpPut("stopTyping")]
        //public async Task StopTyping()
        //{
        //    var userId = long.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //    var user = await _personService.GetByIdAsync(userId);
        //    if (user == null)
        //    {
        //        BadRequest("User not found.");
        //    }
        //    else
        //        await _hubContext.Clients.All.SendAsync("StopTyping", user.UserName);
        //}
    }
}
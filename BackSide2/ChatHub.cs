using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Auga.BL.UsersConnections;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Auga
{
    //[Authorize]
    public class ChatHub : Hub
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConnectionMapping _connectionMapping;

        public ChatHub(IHttpContextAccessor httpContextAccessor, IConnectionMapping connectionMapping)
        {
            _httpContextAccessor = httpContextAccessor;
            _connectionMapping = connectionMapping;
        }


        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.SendAsync(name, message);
        }

        //[AllowAnonymous]
        public override async Task OnConnectedAsync()
        {
            var identity = Context.User.Identity;
            var connectionId = Context.ConnectionId;
            if (identity.IsAuthenticated)
            {
                try
                {
                    await _connectionMapping.Add(connectionId);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                await Groups.AddToGroupAsync(Context.ConnectionId, groupName: (identity.Name)).ConfigureAwait(false);
            }

            //await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            var identity = Context.User.Identity;
            var connectionId = Context.ConnectionId;
            if (identity.IsAuthenticated)
            {
                await _connectionMapping.Remove(connectionId);
                //await Groups.AddToGroupAsync(Context.ConnectionId, groupName: (identity.Name)).ConfigureAwait(false);
            }

            //await Clients.All.SendAsync("SendAction", Context.User.Identity.Name, "left");
        }

        [Authorize]
        public async Task AddMessage(string message, MessageReceivedContext receivedContext)
        {
            var asd = receivedContext.Request.Headers["Accept"];

            await Clients.All.SendAsync("SendAction", message);
        }

        [Authorize]
        [HttpPost("joinGame")]
        public async Task JoinGame([Required] int id, MessageReceivedContext receivedContext)
        {
            //IReadOnlyList<IOnlineClient> allByUserId = _onlineClientManager.GetAllByUserId(new UserIdentifier(null, 1));
            //List<string> allConnectionIdForUser = allByUserId.Select(x => x.ConnectionId).ToList();


            //await Clients.Clients(allConnectionIdForUser).SendAsync("getMessage", new ChatMessage(message, AbpSession.UserId));
            //await Clients.User()


            var asd = Context;

            await Clients.All.SendAsync("SendAction", id);
        }

        [Authorize]
        public async Task LeaveGame([Required] int id, MessageReceivedContext receivedContext)
        {
            //IReadOnlyList<IOnlineClient> allByUserId = _onlineClientManager.GetAllByUserId(new UserIdentifier(null, 1));
            //List<string> allConnectionIdForUser = allByUserId.Select(x => x.ConnectionId).ToList();


            //await Clients.Clients(allConnectionIdForUser).SendAsync("getMessage", new ChatMessage(message, AbpSession.UserId));
            //await Clients.User()


            var asd = Context;

            await Clients.All.SendAsync("SendAction", id);
        }

        [Authorize]
        [HttpGet("getGame")]
        public async Task GetGame([Required] int id, MessageReceivedContext receivedContext)
        {
            //IReadOnlyList<IOnlineClient> allByUserId = _onlineClientManager.GetAllByUserId(new UserIdentifier(null, 1));
            //List<string> allConnectionIdForUser = allByUserId.Select(x => x.ConnectionId).ToList();


            //await Clients.Clients(allConnectionIdForUser).SendAsync("getMessage", new ChatMessage(message, AbpSession.UserId));
            //await Clients.User()


            var asd = Context;

            await Clients.All.SendAsync("SendAction", id);
        }

        [Authorize]
        public async Task AddMessage1(string message, MessageReceivedContext receivedContext)
        {
            var asd = Context;
            await Clients.All.SendAsync("SendAction", message);
        }


        public async Task IsOnline(long userId, MessageReceivedContext receivedContext)
        {
            //var context = GlobalHost.ConnectionManager.GetHubContext<SampleHub>();
            //var clients = context.Clients.All;

            var clients = Context.Items.Values;
            var clients1 = Context.Items.Keys;

            var asd = Context;
            await Clients.All.SendAsync("SendAction", "asd");
        }

        //public async Task Send(string message)
        //{
        //    await Clients.All.SendAsync("SendMessage", Context.User.Identity.Name, message);
        //}
    }
}
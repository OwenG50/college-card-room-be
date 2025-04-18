using CollegeCardroomAPI.Models;
using Microsoft.AspNetCore.SignalR;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using System.Threading.Tasks;

namespace CollegeCardroomAPI.Hubs
{
    public class PokerRoomHub : Hub
    {
        public async Task SendWelcomeMessage()
        {
            await Clients.Caller.SendAsync("ReceiveMessage", "Welcome to the room!");
        }

        public async Task NotifyNewPlayerJoined(string userName)
        {
            await Clients.Others.SendAsync("ReceiveMessage", $"New player joined, {userName}");
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined the group {groupName}.");
        }

        public async Task BroadcastLobbyUpdate(Lobby lobby)
        {
            await Clients.Group(lobby.LobbyId.ToString()).SendAsync("LobbyUpdated", lobby);
        }

        public async Task BroadcastLobbyCreate(Lobby lobby)
        {
            await Clients.Group(lobby.LobbyId.ToString()).SendAsync("LobbyCreated", lobby);
        }

        public async Task BroadcastLobbyDelete(int lobbyId)
        {
            await Clients.All.SendAsync("LobbyDeleted", lobbyId);
        }

        public async Task BroadcastGameStart(int lobbyId)
        {
            await Clients.All.SendAsync("GameStarted", lobbyId);
        }

    }
}
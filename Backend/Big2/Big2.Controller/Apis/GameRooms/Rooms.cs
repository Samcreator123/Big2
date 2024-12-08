

namespace Big2.Controller.Apis.GameRooms;

public class Rooms
{
    private readonly List<Room> _rooms = new List<Room>();

    public void AddRoom(Guid gameId, Guid creatorId, string creatorConnId)
    {
        Room room = new() { GameId = gameId };

        room.Players.Add((creatorId, creatorConnId));

        _rooms.Add(room);
    }


    public void JoinRoom(Guid gameId, Guid playerId, string connectionId)
    {
        Room? room = _rooms.FirstOrDefault(o => o.GameId == gameId) 
            ?? throw new NotFoundGameException($"找不到遊戲 {gameId}");

        room.Players.Add((playerId, connectionId));
    }

    public void LeftRoom(string connectionId)
    {
        Room? room = _rooms.FirstOrDefault(o => o.Players.Any(p => p.connectionId == connectionId))
            ?? throw new NotFoundPlayerException($"無連線Id為 {connectionId}");

        var player = room.Players.FirstOrDefault(p => p.connectionId == connectionId);

        if (player != default)
        {
            room.Players.Remove(player);
        }
        else
        {
            throw new NotFoundPlayerException($"無連線Id為 {connectionId}");
        }

        if (room.Players.Count == 0)
        {
            _rooms.Remove(room);
        }
    }

    public List<(Guid gameId ,Guid playerId, string connectionId)> GetPlayers(string connectionId)
    {
        List<(Guid gameId, Guid playerId, string connectionId)> players = [];

        Room? room = _rooms.FirstOrDefault(o => o.Players.Any(p => p.connectionId == connectionId))
            ?? throw new NotFoundPlayerException($"無連線Id為 {connectionId}");

        foreach (var (playerId, connId) in room.Players)
        {
            players.Add((room.GameId, playerId, connId));
        }

        return players;
    }

    internal class Room
    {
        internal Guid GameId { get; set; }

        internal List<(Guid Id, string connectionId)> Players { get; set; } = [];
    }
}

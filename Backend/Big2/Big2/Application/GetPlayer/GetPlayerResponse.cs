using Big2.Domain.Players;

namespace Big2.Application.GetPlayer
{
    public record GetPlayerResponse(
        Player? Player,
        GetPlayerState State,
        string AdditionalMessage = "")
    {
    }
}

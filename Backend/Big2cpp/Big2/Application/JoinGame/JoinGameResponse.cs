using Big2.Domain.Games;
using Big2.Domain.Games.Enums;
using Big2.Domain.Services.Rules.CardCombinationRules;

namespace Big2.Application.JoinGame
{
    public record JoinGameResponse(
        Guid ID,
        string Name,
        bool IncludeJoker,
        int MaxPlayers,
        bool PlayUntilLast,
        List<CardCombinationTypes> ValidHandtypes,
        List<Player> Players,
        GameState GameState,
        JoinGameState JoinGameState,
        string AdditionalMessage)
    {
    }
}

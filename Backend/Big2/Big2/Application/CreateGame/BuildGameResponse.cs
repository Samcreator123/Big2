using Big2.Domain.Games;
using Big2.Domain.Games.Enums;
using Big2.Domain.Services.Rules.CardCombinationRules;

namespace Big2.Application.CreateGame
{
    public record BuildGameResponse(
        Guid ID,
        string Name,
        bool IncludeJoker,
        int MaxPlayers,
        bool PlayUntilLast,
        List<CardCombinationTypes> ValidHandtypes,
        Player Creator,
        GameState GameState,
        BuildGameState BuildGameState,
        string AdditionalMessage)
    {
    }


}

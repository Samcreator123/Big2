using Big2.Domain.Services.Rules.CardCombinationRules;

namespace Big2.Application.CreateGame
{
    public record BuildGameRequest(
        string GameName,
        string CreatorName,
        bool IncludeJoker,
        int MaxPlayers,
        bool PlayUntilLast,
        List<CardCombinationTypes> Handtypes)
    {
    }
}

using Big2.Domain.Decks;

namespace Big2.Application.PlayCards
{
    public record PlayCardsResponse(
        Guid GameID,
        Guid PlayerID,
        PlayCardsState State,
        string additionMessage = "")
    {
    }
}

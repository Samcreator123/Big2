using Big2.Domain.Services.Decks;

namespace Big2.Application.PlayCards
{
    public record PlayCardsRequest(
        Guid GameID,
        Guid PlayerID,
        List<Card> Cards,
        bool IsPass)
    {
    }
}

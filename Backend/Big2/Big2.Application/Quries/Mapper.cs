using Big2.Domain.ValueObjects;

namespace Big2.Application.Quries;
public class Mapper
{
    public static GameViewModel MappingGameViewModel(Game game)
    {
        var customSetting = game.CustomSetting;

        return new GameViewModel
        (
            game.Id,
            new CustomSettingViewModel(customSetting.Name,
                customSetting.IncludeJoker,
                customSetting.MaxPlayers,
                customSetting.PlayUntilLast,
                customSetting.AllowedHandTypes.Select(t => t.ToString()).ToList()),
            game.GameState.ToString(),
            game.Players.Select(MappingPlayerViewModel).ToList(),
            game.CurrentPlayer
        );
    }

    public static PlayerViewModel MappingPlayerViewModel(Player player)
    {
        var vaildHand = player.CurrentHandNullAllowed;

        return new PlayerViewModel
        (
            player.Id,
            player.GameId,
            player.Name,
            player.State.ToString(),
            MappingCards(player.Cards),
            vaildHand is null ? null : MappingValidHand(vaildHand),
            player.Score,
            player.HasPass
        );
    }

    private static ValidHandViewModel MappingValidHand(ValidHand hand)
    {
        return new ValidHandViewModel(
            hand.Type.ToString(),
            MappingCards(hand.Cards));
    }

    private static List<CardViewModel> MappingCards(IEnumerable<Card> cards)
    {
        return cards
            .Select(c => new CardViewModel(c.Rank.ToString(), c.Suit.ToString()))
            .ToList();
    }
}

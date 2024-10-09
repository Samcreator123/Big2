namespace Big2.Application.PlayCards
{
    public enum PlayCardsState
    {
        Success,
        CardNotInHand,
        InvalidCardCombination,
        InvalidFirstCardPlay,
        PlayedCombinationTooWeak,
        InvalidTurn,
        NullCurrentCombination,
        UncomparableCombination,
        UnknownException
    }
}

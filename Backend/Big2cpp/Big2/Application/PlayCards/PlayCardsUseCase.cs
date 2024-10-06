using Big2.Domain.Players;
using Big2.Domain.Players.Exceptions;
using Big2.Helpers.Application;

namespace Big2.Application.PlayCards
{
    public class PlayCardsUseCase(IPlayCardsRepository repository, ILog log)
    {
        public PlayCardsResponse Execute(PlayCardsRequest request)
        {
            try
            {
                log.LogTrace($"玩家 {request.PlayerID} 於遊戲 {request.GameID} 出牌", request);

                Player player = repository.GetCurrentPlayer(request.GameID);

                player.PlayCard(request.Cards, request.IsPass);

                repository.Save(player);

                log.LogTrace($"玩家 {request.PlayerID} 於遊戲 {request.GameID} 出牌成功", request);

                return GetSuccessfulResponse(request);
            }
            catch (PlayCardException ex)
            {
                log.LogWarning(ex.ToString(), ex);

                return GetFailedResponse(request, GetPlayCardsState(ex) , ex.ToString());
            }
            catch (Exception unknownException)
            {
                log.LogError("加入遊戲時發生意料外的錯誤", request, unknownException);

                return GetFailedResponse(request, PlayCardsState.UnknownException, unknownException.ToString());
            }
        }

        private static PlayCardsResponse GetFailedResponse(PlayCardsRequest request, PlayCardsState state, string additionalMessage = "")
        {
            return new(
                request.GameID,
                request.PlayerID,
                state,
                additionalMessage);
        }

        private static PlayCardsResponse GetSuccessfulResponse(PlayCardsRequest request, string additionalMessage = "")
        {
            return new(
                request.GameID,
                request.PlayerID,
                PlayCardsState.Success,
                additionalMessage);
        }

        private static PlayCardsState GetPlayCardsState(PlayCardException playCardException)
        {
            return playCardException switch
            {
                CardNotInHandException => PlayCardsState.CardNotInHand,
                InvalidCardCombinationException => PlayCardsState.InvalidCardCombination,
                InvalidFirstCardPlayException => PlayCardsState.InvalidFirstCardPlay,
                PlayedCombinationTooWeakException => PlayCardsState.PlayedCombinationTooWeak,
                InvalidTurnException => PlayCardsState.InvalidTurn,
                NullCurrentCombinationException => PlayCardsState.NullCurrentCombination,
                UncomparableCombinationException => PlayCardsState.UncomparableCombination,
                _ => PlayCardsState.UnknownException // 預設情況
            };
        }
    }
}

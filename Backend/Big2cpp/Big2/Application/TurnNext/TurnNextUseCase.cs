using Big2.Domain.Games;
using Big2.Domain.Games.Exceptions;
using Big2.Helpers.Application;

namespace Big2.Application.TurnNext
{
    public class TurnNextUseCase(ITurnNextRepository repository, ILog log)
    {
        public TurnNextResponse Execute(TurnNextRequest request)
        {
            try
            {
                log.LogTrace($"玩家 {request.ThisPlayerID} 結束出牌，切換下一個玩家");

                Game game = repository.FindGameByID(request.ThisPlayerID);

                Guid nextPlayerID = game.GetNextPlayer(request.ThisPlayerID).ID;

                log.LogTrace($"玩家 {nextPlayerID} 為遊戲的下一個出牌玩家");

                return GetSuccessfulResponse(game.ID, nextPlayerID, TurnNextState.Success);

            }
            catch (UnknownPlayerIDException ex)
            {
                log.LogWarning(ex.ToString());
                return GetFailedReponse(request.GameID, request.ThisPlayerID, TurnNextState.UnknownPlayerID, ex.ToString());
            }
            catch (OnlyOnePlayerException ex)
            {
                log.LogWarning(ex.ToString());
                return GetFailedReponse(request.GameID, request.ThisPlayerID, TurnNextState.OnlyOnePlayer, ex.ToString());
            }
            catch (Exception unknownException)
            {
                log.LogError($"確認遊戲是否結束發生意料之外的錯誤", request, unknownException);
                return GetFailedReponse(request.GameID, request.ThisPlayerID, TurnNextState.UnknownException, unknownException.ToString());
            }
        }

        private static TurnNextResponse GetFailedReponse(
            Guid gameID,
            Guid thisPlayerID,
            TurnNextState state,
            string additionalMessage = "")
        {
            return new(
                gameID,
                thisPlayerID,
                state,
                additionalMessage);
        }

        private static TurnNextResponse GetSuccessfulResponse(
                Guid gameID,
                Guid nextPlayerID,
                TurnNextState state,
                string additionalMessage = "")
        {
            return new(
                gameID,
                nextPlayerID,
                state,
                additionalMessage);
        }
    }
}

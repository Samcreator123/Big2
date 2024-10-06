using Big2.Domain.Games;
using Big2.Domain.Games.Exceptions;
using Big2.Helpers.Application;

namespace Big2.Application.CheckStartConditions
{
    public class CheckStartConditionsUseCase(ICheckStartConditionsRepository repository, ILog log)
    {
        public CheckStartConditionsResponse Execute(CheckStartConditionsRequest request)
        {
            try
            {
                log.LogTrace($"玩家 {request.PlayerID} 已準備完成，開始檢查遊戲是否可以開始");

                Game game = repository.FindGameByPlayerID(request.PlayerID);

                var playerIDs = repository.FindPreparedPlayerInGame(game.ID);

                bool canStart = game.CanStart(playerIDs);

                if (canStart)
                {
                    log.LogTrace($"遊戲 {game.ID} 可以開始");
                    return GetSuccessfuldReponse(game.ID);
                }
                else
                {
                    log.LogTrace($"遊戲 {game.ID} 還無法開始");
                    return GetFailedReponse(game.ID, CheckStartConditionsState.CanNotStart, $"遊戲 {game.ID} 還無法開始");
                }
            }
            catch (UnknownPlayerIDException ex)
            {
                log.LogWarning(ex.ToString());

                return GetFailedReponse(Guid.Empty, CheckStartConditionsState.UnknownPlayerIDException, ex.ToString());
            }
            catch (Exception unknownException)
            {
                log.LogError($"確認遊戲是否可開始發生意料之外的錯誤", request, unknownException);
                return GetFailedReponse(Guid.Empty, CheckStartConditionsState.UnknownException, unknownException.ToString());
            }
        }

        private static CheckStartConditionsResponse GetFailedReponse(
            Guid gameID,
            CheckStartConditionsState state,
            string additionalMessage = "")
        {
            return new(
                gameID,
                state,
                additionalMessage);
        }

        private static CheckStartConditionsResponse GetSuccessfuldReponse(
                Guid gameID,
                string additionalMessage = "")
        {
            return new(
                gameID,
                CheckStartConditionsState.CanStart,
                additionalMessage);
        }
    }
}

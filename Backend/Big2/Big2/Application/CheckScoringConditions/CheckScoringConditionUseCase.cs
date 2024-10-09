using Big2.Domain.Games;
using Big2.Domain.Games.Exceptions;
using Big2.Helpers.Application;

namespace Big2.Application.CheckScoringConditions
{
    public class CheckScoringConditionUseCase(ICheckScoringConditionRepository repository, ILog log)
    {
        public CheckScoringConditionResponse Execute(CheckScoringConditionsRequest request)
        {
            try
            {
                log.LogInfo($"玩家 {request.FinishedPlayerID} 卡牌為空，開始檢查遊戲 {request.GameID} 是否結束");

                Game game = repository.FindGameByID(request.GameID);

                game.UpdateWhenPlayerEnd(request.FinishedPlayerID);

                if (game.IsEnd)
                {
                    game.SetScoring();
                    return GetSuccessfuldReponse(game.ID, CheckScoringConditionState.GameEnded);
                }
                else
                {
                    return GetSuccessfuldReponse(game.ID, CheckScoringConditionState.GameContinued);
                }
            }
            catch (UnknownPlayerIDException ex)
            {
                log.LogWarning(ex.ToString());
                return GetFailedReponse(Guid.Empty, CheckScoringConditionState.UnknownPlayerIDException, ex.ToString());
            }
            catch (Exception unknownException)
            {
                log.LogError($"確認遊戲是否結束發生意料之外的錯誤", request, unknownException);
                return GetFailedReponse(Guid.Empty, CheckScoringConditionState.UnknownException, unknownException.ToString());
            }
        }

        private static CheckScoringConditionResponse GetFailedReponse(
            Guid gameID,
            CheckScoringConditionState state,
            string additionalMessage = "")
        {
            return new(
                gameID,
                state,
                additionalMessage);
        }

        private static CheckScoringConditionResponse GetSuccessfuldReponse(
                Guid gameID,
                CheckScoringConditionState state,
                string additionalMessage = "")
        {
            return new(
                gameID,
                state,
                additionalMessage);
        }
    }
}

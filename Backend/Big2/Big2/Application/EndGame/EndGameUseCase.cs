using Big2.Domain.Games;
using Big2.Domain.Games.Exceptions;
using Big2.Helpers.Application;

namespace Big2.Application.EndGame
{
    public class EndGameUseCase(IEndGameRepository repository, ILog log)
    {
        public EndGameResponse Execute(EndGameRequest request)
        {
            try
            {
                log.LogInfo($"開始結束遊戲 {request.GameID}", request);

                Game game = repository.GetGame(request.GameID);

                game.SetWaiting();

                repository.Save(game);

                log.LogInfo($"結束遊戲 {request.GameID} 成功", request);

                return GetSuccessfulResponse(game.ID);
            }
            catch (InvalidGameStateException ex)
            {
                return GetFailedResponse(request.GameID, EndGameState.InvalidGameState, ex.ToString());
            }
            catch (Exception unknownException)
            {
                log.LogError("建立遊戲時發生意料外的錯誤", request, unknownException);

                return GetFailedResponse(request.GameID, EndGameState.UnknownException, unknownException.ToString());
            }
        }

        private static EndGameResponse GetFailedResponse(Guid gameID, EndGameState state, string additionalMessage = "")
        {
            return new(
                gameID,
                state,
                additionalMessage);
        }

        private static EndGameResponse GetSuccessfulResponse(Guid gameID, string additionalMessage = "")
        {
            return new(
                gameID,
                EndGameState.Success,
                additionalMessage);
        }
    }
}

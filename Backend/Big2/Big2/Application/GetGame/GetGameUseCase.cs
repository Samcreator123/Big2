using Big2.Domain.Games;
using Big2.Helpers.Application;

namespace Big2.Application.GetGame
{
    public class GetGameUseCase(IGetGameRepository repository, ILog log)
    {
        public GetGameResponse Execute(GetGameRequest request)
        {
            try
            {
                log.LogInfo($"試圖取得遊戲 {request.GameID}", request);

                Game? game = repository.GetGame(request.GameID);

                if (game == null)
                {
                    log.LogInfo($"遊戲 {request.GameID} 不存在");
                    return GetFailedResponse(null, GetGameState.NotFound);
                }


                log.LogInfo($"取得遊戲 {request.GameID}", game);

                return GetSuccessfulResponse(game);
            }
            catch (Exception unknownException)
            {
                log.LogError($"取得 {request.GameID} 時發生意料之外的錯誤", request, unknownException);

                return GetFailedResponse(null, GetGameState.UnknownException, unknownException.ToString());
            }
        }

        private static GetGameResponse GetFailedResponse(Game? game, GetGameState state, string additionalMessage = "")
        {
            return new(
                game,
                state,
                additionalMessage);
        }

        private static GetGameResponse GetSuccessfulResponse(Game game, string additionalMessage = "")
        {
            return new(
                game,
                GetGameState.Success,
                additionalMessage);
        }
    }
}

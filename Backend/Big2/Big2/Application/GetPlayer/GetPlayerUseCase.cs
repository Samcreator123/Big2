using Big2.Domain.Players;
using Big2.Helpers.Application;

namespace Big2.Application.GetPlayer
{
    public class GetPlayerUseCase(IGetPlayerRepository repository, ILog log)
    {
        public GetPlayerResponse Execute(GetPlayerRequest request)
        {
            try
            {
                log.LogInfo($"試圖取得遊戲 {request.PlayerID}", request);

                Player? player = repository.GetPlayer(request.PlayerID);

                if (player == null)
                {
                    log.LogInfo($"遊戲 {request.PlayerID} 不存在");
                    return GetFailedResponse(null, GetPlayerState.NotFound);
                }


                log.LogInfo($"取得遊戲 {request.PlayerID}", player);

                return GetSuccessfulResponse(player);
            }
            catch (Exception unknownException)
            {
                log.LogError($"取得 {request.PlayerID} 時發生意料之外的錯誤", request, unknownException);

                return GetFailedResponse(null, GetPlayerState.UnknownException, unknownException.ToString());
            }
        }

        private static GetPlayerResponse GetFailedResponse(Player? Player, GetPlayerState state, string additionalMessage = "")
        {
            return new(
                Player,
                state,
                additionalMessage);
        }

        private static GetPlayerResponse GetSuccessfulResponse(Player Player, string additionalMessage = "")
        {
            return new(
                Player,
                GetPlayerState.Success,
                additionalMessage);
        }
    }
}

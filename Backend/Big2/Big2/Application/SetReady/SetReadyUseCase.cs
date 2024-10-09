using Big2.Application.SetPlayerReady;
using Big2.Domain.Players;
using Big2.Domain.Players.Exceptions;
using Big2.Helpers.Application;

namespace Big2.Application.SetReady
{
    public class SetReadyUseCase(ISetReadyRepository repository, ILog log)
    {
        public SetReadyResponse Execute(SetReadyRequest request)
        {
            try
            {
                Player player = repository.FindPlayerByID(request.PlayerID);

                player.SetReady();

                repository.SetPlayerReady(player);

                log.LogInfo($"玩家 {request.PlayerID} 準備完畢", request);

                return GetSuccessfulResponse(request);
            }
            catch (InvalidPlayerStateException ex)
            {
                return GetFailedResponse(request, SetReadyState.InvalidPlayerStateException, ex.ToString());
            }
            catch (Exception unknownException)
            {
                log.LogError($"{request.PlayerID}加入遊戲時發生意料外的錯誤", request, unknownException);

                return GetFailedResponse(request, SetReadyState.UnknownException, unknownException.ToString());
            }
        }

        private static SetReadyResponse GetFailedResponse(SetReadyRequest request, SetReadyState state, string additionalMessage = "")
        {
            return new(
                request.PlayerID,
                false,
                state,
                additionalMessage);
        }

        private static SetReadyResponse GetSuccessfulResponse(SetReadyRequest request, string additionalMessage = "")
        {
            return new(
                request.PlayerID,
                true,
                SetReadyState.Success,
                additionalMessage);
        }
    }
}

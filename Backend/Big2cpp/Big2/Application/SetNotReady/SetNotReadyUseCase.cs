using Big2.Domain.Players;
using Big2.Domain.Players.Exceptions;
using Big2.Helpers.Application;

namespace Big2.Application.SetNotReady
{

    public class SetNotReadyUseCase(ISetNotReadyRepository repository, ILog log)
    {
        public SetNotReadyResponse Execute(SetNotReadyRequest request)
        {
            try
            {
                Player player = repository.FindPlayerByID(request.PlayerID);

                player.SetNotReady();

                repository.SetPlayerNotReady(player);

                log.LogTrace($"玩家 {request.PlayerID} 取消準備完畢", request);

                return GetSuccessfulResponse(request);
            }
            catch (InvalidPlayerStateException ex)
            {
                return GetFailedResponse(request, SetNotReadyState.InvalidPlayerStateException, ex.ToString());
            }
            catch (Exception unknownException)
            {
                log.LogError($"玩家 {request.PlayerID} 取消準備完畢時發生意料之外的錯誤", request, unknownException);

                return GetFailedResponse(request, SetNotReadyState.UnknownException, unknownException.ToString());
            }
        }

        private static SetNotReadyResponse GetFailedResponse(SetNotReadyRequest request, SetNotReadyState state, string additionalMessage = "")
        {
            return new(
                request.PlayerID,
                false,
                state,
                additionalMessage);
        }

        private static SetNotReadyResponse GetSuccessfulResponse(SetNotReadyRequest request, string additionalMessage = "")
        {
            return new(
                request.PlayerID,
                true,
                SetNotReadyState.Success,
                additionalMessage);
        }
    }
}


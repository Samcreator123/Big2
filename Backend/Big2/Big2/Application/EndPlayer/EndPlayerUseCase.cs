using Big2.Domain.Players;
using Big2.Domain.Players.Exceptions;
using Big2.Helpers.Application;

namespace Big2.Application.EndPlayer
{
    public class EndPlayerUseCase(IEndPlayerRepository repository, ILog log)
    {
        public EndPlayerResponse Execute(EndPlayerRequest request)
        {
            try
            {
                log.LogInfo($"玩家結束遊戲 {request.PlayerID}", request);

                Player player = repository.GetPlayer(request.PlayerID);
                player.SetNotReady();

                repository.Save(player);

                log.LogInfo($"玩家結束遊戲 {request.PlayerID} 成功", request);

                return GetSuccessfulResponse(player.ID);
            }
            catch (InvalidPlayerStateException ex)
            {
                return GetFailedResponse(request.PlayerID, EndPlayerState.InvalidPlayerState, ex.ToString());
            }
            catch (Exception unknownException)
            {
                log.LogError("建立遊戲時發生意料外的錯誤", request, unknownException);

                return GetFailedResponse(request.PlayerID, EndPlayerState.UnknownException, unknownException.ToString());
            }
        }

        private static EndPlayerResponse GetFailedResponse(Guid gameID, EndPlayerState state, string additionalMessage = "")
        {
            return new(
                gameID,
                state,
                additionalMessage);
        }

        private static EndPlayerResponse GetSuccessfulResponse(Guid gameID, string additionalMessage = "")
        {
            return new(
                gameID,
                EndPlayerState.Success,
                additionalMessage);
        }
    }
}

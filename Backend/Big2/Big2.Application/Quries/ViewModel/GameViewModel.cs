namespace Big2.Application.Quries.ViewModel;

public record GameViewModel(Guid Id,
    CustomSettingViewModel CustomSetting,
    string GameState,
    List<PlayerViewModel> Players,
    Player CurrentPlayer)
{
}


namespace Big2.Application.Quries.ViewModel;
public record CustomSettingViewModel(string Name,
    bool IncludeJoker,
    int MaxPlayers,
    bool PlayUntilLast,
    List<string> AllowHandTypes)
{

}

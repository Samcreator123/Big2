namespace Big2.Domain.Entities;
public class GameCustomSetting : Entity
{
    public string Name { get; private set; } = string.Empty;

    public bool IncludeJoker { get; private set; }

    public int MaxPlayers { get; private set; }

    public bool PlayUntilLast { get; private set; }

    public List<ValidHandType> AllowedHandTypes { get; private set; } = [];

    public GameCustomSetting(string name, bool includeJoker, int maxPlayers,
        bool playUntilLast, List<ValidHandType> allowedHandTypes)
    {
        Name = name;
        IncludeJoker = includeJoker;
        MaxPlayers = maxPlayers;
        PlayUntilLast = playUntilLast;
        AllowedHandTypes = allowedHandTypes;
    }

    public GameCustomSetting(CustomSettingMemento memento)
    {

    }
}

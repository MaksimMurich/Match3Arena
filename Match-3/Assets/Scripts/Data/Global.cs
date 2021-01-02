using Leopotam.Ecs;
using Match3;
using Match3.Assets.Scripts.Services;
using Match3.Assets.Scripts.Services.SaveLoad;
using Match3.Configurations;

public static class Global
{
    public static readonly Configurations Config = new Configurations();
    public static PlayerPreferences Preferences;
    public static readonly GlobalData Data = new GlobalData();
    public static readonly ViewsContainer Views = new ViewsContainer();
    public static readonly ServicesContainer Services = new ServicesContainer();

    static Global()
    {
        Preferences = LocalSaveLoad<PlayerPreferences>.Load();

        if (Preferences == null)
        {
            Preferences = new PlayerPreferences();
        }
    }

    public class GlobalData
    {
        public InGameData InGame = new InGameData();
        public LobbyData Lobby = new LobbyData();
        public CommonData Common;
        public PlayerData Player;
    }

    public class Configurations
    {
        public InGameConfiguration InGame;
        public LobbyConfiguration Lobby;
    }

    public class InGameData
    {
        public EcsWorld World;
        public EcsSystems Systems;
        public PlayerState PlayerState;
        public GameField GameField = new GameField();
    }

    public class LobbyData
    {
        public EcsWorld World;
    }

    public class CommonData
    {

    }

    public class ViewsContainer
    {
        public InGameViews InGame;
        public LobbyViews Lobby;
    }
}
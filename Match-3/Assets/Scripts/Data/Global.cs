using Leopotam.Ecs;
using Match3;
using Match3.Assets.Scripts.Services;
using Match3.Assets.Scripts.Services.SaveLoad;
using Match3.Configurations;

public class Global
{
    public static readonly Configurations Config = new Configurations();
    public static readonly GlobalData Data = new GlobalData();
    public static readonly ViewsContainer Views = new ViewsContainer();
    public static readonly ServicesContainer Services = new ServicesContainer();

    public class GlobalData
    {
        public InGameData InGame = new InGameData();
        public LobbyData Lobby;
        public CommonData Common;
        public PlayerData Player;
    }

    public class Configurations
    {
        public InGameConfiguration InGame;
        //TODO public LobbyConfiguration InGame;
        //TODO public CommonConfiguration InGame;
    }

    public class InGameData
    {
        public EcsWorld World;
        public EcsSystems Systems;
        public PlayerState PlayerState;

        public readonly GameField GameField = new GameField();
    }

    public class LobbyData
    {

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
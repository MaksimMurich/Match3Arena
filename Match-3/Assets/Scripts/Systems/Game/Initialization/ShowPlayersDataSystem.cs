using Leopotam.Ecs;
using Match3.Assets.Scripts.Services.SaveLoad;

namespace Match3.Systems.Game.Initialization
{
    public sealed class ShowPlayersDataSystem : IEcsInitSystem
    {
        private readonly PlayerData _playerData = null;
        private readonly InGameViews _sceneData = null;

        private PlayerPreferences _playerPreferences;

        public void Init()
        {
            _playerPreferences = LocalSaveLoad<PlayerPreferences>.Load();

            Global.Views.InGame.PlayerDataView.Nick.text = _playerPreferences.Nick;
            Global.Views.InGame.BotDataView.Nick.text = OpponentState.Nick;

            Global.Views.InGame.BotDataView.Rating.text = OpponentState.Rating.ToString();
            Global.Views.InGame.PlayerDataView.Rating.text = Global.Data.Player.Rating.ToString();
        }
    }
}
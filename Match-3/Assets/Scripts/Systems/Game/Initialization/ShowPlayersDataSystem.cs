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

            _sceneData.PlayerDataView.Nick.text = _playerPreferences.Nick;
            _sceneData.BotDataView.Nick.text = OpponentState.Nick;

            _sceneData.BotDataView.Rating.text = OpponentState.Rating.ToString();
            _sceneData.PlayerDataView.Rating.text = Global.Data.Player.Rating.ToString();
        }
    }
}
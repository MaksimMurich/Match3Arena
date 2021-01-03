using Leopotam.Ecs;

namespace Match3.Systems.Game.Initialization
{
    public sealed class ShowPlayersDataSystem : IEcsInitSystem
    {
        public void Init()
        {
            Global.Views.InGame.PlayerDataView.Nick.text = Global.Preferences.Nick;
            Global.Views.InGame.BotDataView.Nick.text = OpponentState.Nick;

            Global.Views.InGame.BotDataView.Rating.text = OpponentState.Rating.ToString();
            Global.Views.InGame.PlayerDataView.Rating.text = Global.Data.Player.Rating.ToString();
        }
    }
}
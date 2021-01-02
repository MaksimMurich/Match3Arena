using Leopotam.Ecs;
using Match3.Assets.Scripts.Services.SaveLoad;
using Match3.Components.Game.Events;

namespace Match3.Assets.Scripts.Systems.Game.UI
{
    public sealed class EndRoundRewardPlayerSystem : IEcsRunSystem
    {
        private readonly PlayerData _playerData = null;
        private readonly PlayerState _playerState = null;

        private readonly EcsFilter<EndRoundRequest> _filter = null;

        public void Run()
        {
            if (_filter.GetEntitiesCount() == 0)
            {
                return;
            }

            bool win = _playerState.CurrentLife > 0;

            if (win)
            {
                Global.Data.Player.Coins += _playerState.CurrentBet * 2;

                Global.Data.Player.Rating += _playerState.DeltaRatingReward + _playerState.DeltaRatingUnreward;

                Global.Data.Player.WinsCount++;
            }

            Global.Data.Player.RoundsCount++;

            LocalSaveLoad<PlayerData>.Save(_playerData);
        }
    }
}

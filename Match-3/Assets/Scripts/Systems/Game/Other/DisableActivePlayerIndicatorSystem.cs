using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game
{
    public sealed class DisableActivePlayerIndicatorSystem : IEcsRunSystem
    {
        private readonly InGameViews _inGameSceneData = null;
        private readonly EcsFilter<Cell, Vector2Int, AnimateSwapRequest>.Exclude<AnimateSwapBackRequest> _filter = null;

        public void Run()
        {
            if (_filter.GetEntitiesCount() > 0)
            {
                Global.Views.InGame.PlayerDataView.ActivateOutline(false);
                Global.Views.InGame.BotDataView.ActivateOutline(false);
            }
        }
    }
}

using Leopotam.Ecs;
using Match3.Assets.Scripts.Components.Common;
using Match3.Assets.Scripts.UnityComponents.UI.InGame;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.Configurations;

namespace Match3.Assets.Scripts.Systems.Game
{

    public sealed class HighlightCurrentPlayerOutlineSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly PlayerState _playerState = null;
        private readonly InGameViews _inGameSceneData = null;
        private readonly InGameConfiguration _configuration = null;
        private readonly EcsFilter<NextPlayerRequest> _changePlayerFilter = null;
        private readonly EcsFilter<ChangeFieldAnimating> _fieldChangers = null;
        private readonly EcsFilter<AnimateExplosion> _explosionAnimations = null;
        private readonly EcsFilter<ChainEvent> _chains = null;

        private bool _activePlayerChanged;

        public void Run()
        {

            if (_changePlayerFilter.GetEntitiesCount() > 0)
            {
                _activePlayerChanged = true;
                return;
            }

            bool swapLocked = _fieldChangers.GetEntitiesCount() > 0 || _chains.GetEntitiesCount() > 0 || _explosionAnimations.GetEntitiesCount() > 0;

            if (swapLocked || !_activePlayerChanged)
            {
                return;
            }

            IndicateActivePlayer();

            _activePlayerChanged = false;
        }

        private void IndicateActivePlayer()
        {
            if (Global.Data.InGame.PlayerState.Active)
            {
                 Global.Data.InGame.World.NewEntity().Set<PlaySoundRequest>() = new PlaySoundRequest( Global.Config.InGame.Sounds.PlayerTurn);
            }

            PlayerInGameDataView player = GetActivePlayer();
            PlayerInGameDataView inactivePlayer = GetInactivePlayer();
            player.ActivateOutline(true);
            inactivePlayer.ActivateOutline(false);
        }

        private PlayerInGameDataView GetActivePlayer()
        {
            PlayerInGameDataView activePlayer;

            if (Global.Data.InGame.PlayerState.Active)
            {
                activePlayer = Global.Views.InGame.PlayerDataView;
            }
            else
            {
                activePlayer = Global.Views.InGame.BotDataView;
            }

            return activePlayer;
        }

        private PlayerInGameDataView GetInactivePlayer()
        {
            PlayerInGameDataView inactivePlayer;

            if (Global.Data.InGame.PlayerState.Active)
            {
                inactivePlayer = Global.Views.InGame.BotDataView;
            }
            else
            {
                inactivePlayer = Global.Views.InGame.PlayerDataView;
            }

            return inactivePlayer;
        }
    }
}

using DG.Tweening;
using Leopotam.Ecs;
using Match3.Assets.Scripts.Components.Common;
using Match3.Assets.Scripts.UnityComponents.UI.InGame;
using Match3.Components.Game;
using Match3.Configurations;

namespace Match3.Assets.Scripts.Systems.Game
{
    public sealed class HighlightFirstStepPlayerOutlineSystem : IEcsInitSystem
    {
        private EcsEntity changeFieldEntity;

        public void Init()
        {
            changeFieldEntity =  Global.Data.InGame.World.NewEntity();
            changeFieldEntity.Set<ChangeFieldAnimating>();

            PlayerInGameDataView activePlayer = GetActivePlayer();

            Sequence sequence = DOTween.Sequence();
            sequence.SetDelay( Global.Config.InGame.Animation.SelectFirstPlayerDuration);
            sequence.OnComplete(() =>
            {
                changeFieldEntity.Destroy();
                IndicateActivePlayer();
            });
        }

        private void IndicateActivePlayer()
        {
            PlayerInGameDataView player = GetActivePlayer();
            PlayerInGameDataView inactivePlayer = GetInactivePlayer();
            player.ActivateOutline(true);
            inactivePlayer.ActivateOutline(false);

            if (Global.Data.InGame.PlayerState.Active)
            {
                 Global.Data.InGame.World.NewEntity().Set<PlaySoundRequest>() = new PlaySoundRequest( Global.Config.InGame.Sounds.PlayerTurn);
            }
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

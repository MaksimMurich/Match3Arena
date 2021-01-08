using Leopotam.Ecs;
using UnityEngine;
using DG.Tweening;
using Match3.Components.Game.Events;

namespace Match3.Systems.Game {
    sealed class ManageTurnTimeSystem : IEcsRunSystem {
		private readonly EcsFilter<NextPlayerRequest> _filter = null;
        readonly EcsWorld _world = null;

		private float _turnTimer;
        static private Tween _timerTween = null;

		public float TurnTimer { get => _turnTimer; private set => _turnTimer = value; }

		void IEcsRunSystem.Run () {
            if(_filter.GetEntitiesCount() == 0)
			{
                if(_timerTween == null)
				{
                    ResetTimerTween();
                }
                
                if(TurnTimer <= 0)
				{
                    _world.NewEntity().Set<NextPlayerRequest>();
                    ResetTimerTween();
                }
            }
			else
			{
                ResetTimerTween();
            }

            // debug:
            Debug.Log($"Timer value: {TurnTimer}");
        }

        private void ResetTimerTween()
		{
            //DOTween.Kill(_timerTween.id);
            _timerTween = null;
            TurnTimer = Global.Config.InGame.MaxTurnTime;
            _timerTween = DOTween.To(() => TurnTimer, x => TurnTimer = x, 0, Global.Config.InGame.MaxTurnTime);
            
        }
        
    }
}
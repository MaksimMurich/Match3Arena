using Leopotam.Ecs;
using UnityEngine;
using DG.Tweening;
using Match3.Components.Game.Events;

namespace Match3.Systems.Game {
    sealed class ManageTurnTimeSystem : IEcsRunSystem, IEcsInitSystem {
		private readonly EcsFilter<NextPlayerRequest> _nextPlayerRequestsfilter = null;
		private readonly EcsFilter<ResetTurnTimerRequest> _resetTurnTimerRequestsfilter = null;
        private readonly EcsFilter<EndRoundRequest> _endRoundRequestsFilter = null;

        private float _timeRamain = 1; // value>0 needed to avoid starting timer before starting turn
        private bool _isTimerActive = false;

		public void Init()
		{
            Sequence sequence = DOTween.Sequence();
            sequence.SetDelay(Global.Config.InGame.Animation.SelectFirstPlayerDuration);
            sequence.OnComplete(() =>
            {
                ResetTimeRemain();
            });
        }

		void IEcsRunSystem.Run () {
            bool needResetTimer = _resetTurnTimerRequestsfilter.GetEntitiesCount() != 0;
			bool needChangePlayer = _nextPlayerRequestsfilter.GetEntitiesCount() > 0;

			if (needChangePlayer)
			{
                ResetTimeRemain();               
            }
			else
			{
                if (needResetTimer)
                {
                    ResetTimeRemain();
                }

                if (_isTimerActive)// TODO make 1 update in second
                {
                    _timeRamain -= Time.deltaTime;
                    EcsEntity timerUpdateRequest = Global.Data.InGame.World.NewEntity();
                    timerUpdateRequest.Set<UpdateTurnTimerViewRequest>().RemainTime = _timeRamain;
                }

                if (_timeRamain <= 0)
                {
                    Global.Data.InGame.World.NewEntity().Set<NextPlayerRequest>();
                    ResetTimeRemain();
                }
            }
        }

        private void ResetTimeRemain()
		{
            _timeRamain = Global.Config.InGame.MaxTurnTime+2; // additional time over the time, setted in config to make such time in-game
        }
	}
}
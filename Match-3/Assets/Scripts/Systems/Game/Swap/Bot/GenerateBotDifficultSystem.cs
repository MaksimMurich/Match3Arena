using Leopotam.Ecs;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.Swap.Bot {
    public sealed class GenerateBotDifficultSystem : IEcsInitSystem {
        public void Init() {
            float difficultPossibilityPoint = Random.Range(0f, 1f);
            float difficult = Global.Config.InGame.BotBehaviour.DifficultPossibility.Evaluate(difficultPossibilityPoint);
            OpponentState.Difficult = difficult;
            OpponentState.Rating = (int)(Global.Data.Player.Rating * (0.5f + difficult));
            OpponentState.Rating += Random.Range(-100, 100);
            OpponentState.Rating = Mathf.Abs(OpponentState.Rating);
            OpponentState.MaxLife = Global.Config.InGame.PlayersMaxLife;
            OpponentState.CurrentLife = Global.Config.InGame.PlayersMaxLife;
        }
    }
}

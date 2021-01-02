using Leopotam.Ecs;
using Match3.Assets.Scripts.Services.SaveLoad;
using Match3.Configurations;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.Swap.Bot
{
    public sealed class GenerateBotDifficultSystem : IEcsInitSystem
    {
        private readonly PlayerData _playerData = null;
        private readonly InGameConfiguration _configuration = null;

        public void Init()
        {
            float difficultPossibilityPoint = UnityEngine.Random.Range(0f, 1f);
            float difficult = _configuration.BotBehaviour.DifficultPossibility.Evaluate(difficultPossibilityPoint);
            OpponentState.Difficult = difficult;
            OpponentState.Rating = (int)(_playerData.Rating * (0.5f + difficult));
            OpponentState.Rating += Random.Range(-100, 100);
            OpponentState.Rating = Mathf.Abs(OpponentState.Rating);
            OpponentState.MaxLife = _configuration.PlayersMaxLife;
            OpponentState.CurrentLife = _configuration.PlayersMaxLife;
        }
    }
}

using Match3.Assets.Scripts.UnityComponents.UI.InGame;
using UnityEngine;

namespace Match3.Assets.Scripts.Components.Game.Events.Rewards
{
    public struct HealthRewardRequest
    {
        public int Value;
        public Vector2Int Position;
        public CellRewardView View;
    }
}

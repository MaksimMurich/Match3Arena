using Match3.Assets.Scripts.UnityComponents.UI.InGame;
using UnityEngine;

namespace Match3.Assets.Scripts.Components.Game.Events.Rewards
{
    public struct DemageRewardRequest
    {
        public int Value;
        public Vector2Int Position;
        internal CellRewardView View;
    }
}

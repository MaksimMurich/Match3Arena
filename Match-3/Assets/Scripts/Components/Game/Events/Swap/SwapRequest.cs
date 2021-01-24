using System;
using UnityEngine;

namespace Match3.Components.Game.Events {
    [Serializable]
    public struct SwapRequest {
        public Vector2Int From;
        public Vector2Int To;
    }
}

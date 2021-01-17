using Leopotam.Ecs;
using System;

namespace Match3.Assets.Scripts.Systems.Game.Initialization.FirstPlaer {
    public sealed class SelectFirstPlayerSystem : IEcsInitSystem {
        public void Init() {
            Random r = new Random();
            int active = r.Next(0, 2); ;
            Global.Data.InGame.PlayerState.Active = active == 1;
        }
    }
}

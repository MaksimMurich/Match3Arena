using Leopotam.Ecs;
using System;

namespace Match3.Assets.Scripts.Systems.Game.Initialization.FirstPlaer
{
    public sealed class SelectFirstPlayerSystem : IEcsInitSystem
    {
        private readonly PlayerState _playerState = null;

        public void Init()
        {
            Random r = new Random();
            int active = r.Next(0, 2); ;
            _playerState.Active = active == 1;
        }
    }
}

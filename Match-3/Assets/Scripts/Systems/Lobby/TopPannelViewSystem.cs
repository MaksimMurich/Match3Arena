using Leopotam.Ecs;
using System;

namespace Match3.Assets.Scripts.Systems {
    sealed class TopPannelViewSystem : IEcsInitSystem {
        private readonly LobbyTopPannelView topPannel = Global.Views.Lobby.TopPannel;

        public void Init () {
            topPannel.Coins.text = Convert.ToString(Global.Data.Player.Coins);
        }
    }
}
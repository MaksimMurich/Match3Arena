using Leopotam.Ecs;
using Match3.Assets.Scripts.Configurations;
using Match3.Configurations;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Match3.Assets.Scripts.Systems {
    sealed class LaunchGameSystem : IEcsInitSystem {
        public void Init() {
            foreach (var arena in Global.Views.Lobby.Arenas) {
                arena.PlayHandler += ClickedPlayHandler;
            }
        }

        private void ClickedPlayHandler(int id) {
            ArenaConfig config = Global.Config.Lobby.ArenaConfigs.Where(config => config.ID == id).FirstOrDefault();

            if (config == null) {
                Debug.LogError("Arena config is null.");
                config = Global.Config.Lobby.DefaultArenaConfig;
            }

            // Use, when coins whould be configurated.
            //if(Global.Data.Player.Coins < config.Bet) {
            //    return;
            //}

            Debug.Log($"Bet id {config.Bet}. Launching game scene.");
            Global.Data.Common.PlayerState.CurrentBet = config.Bet;
            SceneManager.LoadScene(SceneNameConstants.Game);
        }
    }
}
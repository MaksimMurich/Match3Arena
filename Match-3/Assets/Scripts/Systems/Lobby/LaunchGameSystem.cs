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
            ArenaConfig configuration = Global.Config.Lobby.ArenaConfigs.Where(config => config.ID == id).FirstOrDefault();

            if (configuration == null) {
                Debug.LogError("Arena config is null.");
                configuration = Global.Config.Lobby.DefaultArenaConfig;
            }

            if(Global.Data.Player.Coins < configuration.Bet) {
               return;
            }

            Debug.Log($"Bet id {configuration.Bet}. Launching game scene.");
            Global.Data.Common.PlayerState.CurrentBet = configuration.Bet;
            SceneManager.LoadScene(SceneNameConstants.Game);
        }
    }
}
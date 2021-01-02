using Leopotam.Ecs;
using Match3.Assets.Scripts.Services.Pool;
using Match3.Assets.Scripts.UnityComponents.UI.InGame;
using Match3.Configurations;
using System.Linq;

namespace Match3.Systems.Game.Initialization
{
    public sealed class InitializeCellRewardViewTable : IEcsInitSystem
    {
        private readonly InGameConfiguration _configuration = null;
        private readonly InGameViews _inGameSceneData = null;
        private readonly ObjectPool _objectPool = null;

        public void Init()
        {
            CellConfiguration[] cells =  Global.Config.InGame.CellConfigurations.OrderBy(c => c.Demage + c.Health).Reverse().ToArray();

            for (int i = 0; i < cells.Length; i++)
            {
                CellRewardViewTableItem item = Global.Services.Pool.Get(_inGameSceneData.CellRewardViewTable.CellRewardViewTableItemExample);
                _inGameSceneData.CellRewardViewTable.AddItem(item, cells[i].Health, cells[i].Demage, cells[i].ViewExample.GetSprite());
            }
        }
    }
}
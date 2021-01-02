using Leopotam.Ecs;
using Match3.Assets.Scripts.Services.Pool;
using Match3.Assets.Scripts.UnityComponents.UI.InGame;
using Match3.Configurations;
using System.Linq;

namespace Match3.Systems.Game.Initialization
{
    public sealed class InitializeCellRewardViewTable : IEcsInitSystem
    {
        public void Init()
        {
            CellConfiguration[] cells =  Global.Config.InGame.CellConfigurations.OrderBy(c => c.Demage + c.Health).Reverse().ToArray();

            for (int i = 0; i < cells.Length; i++)
            {
                CellRewardViewTableItem item = Global.Services.Pool.Get(Global.Views.InGame.CellRewardViewTable.CellRewardViewTableItemExample);
                Global.Views.InGame.CellRewardViewTable.AddItem(item, cells[i].Health, cells[i].Demage, cells[i].ViewExample.GetSprite());
            }
        }
    }
}
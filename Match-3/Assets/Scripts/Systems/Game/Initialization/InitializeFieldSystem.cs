using Leopotam.Ecs;
using Match3.Assets.Scripts.Services;
using Match3.Components.Game;
using Match3.Configurations;
using System.Linq;
using UnityEngine;

namespace Match3.Systems.Game.Initialization
{
    public sealed class InitializeFieldSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly InGameConfiguration _configuration = null;
        private readonly GameField _gameField = null;

        public void Init()
        {
            for (int row = 0; row <  Global.Config.InGame.LevelHeight; row++)
            {
                for (int column = 0; column <  Global.Config.InGame.LevelWidth; column++)
                {
                    EcsEntity cellEntity =  Global.Data.InGame.World.NewEntity();
                    Vector2Int position = new Vector2Int(column, row);
                    cellEntity.Set<Vector2Int>() = position;

                    bool hasChain = true;
                    int tryCount = 0;

                    Global.Data.InGame.GameField.Cells.Add(position, cellEntity);

                    while (hasChain && tryCount < 100)
                    {
                        tryCount++;

                        float random = Random.Range(0f, 100f);
                        CellConfiguration configuration =  Global.Config.InGame.CellConfigurations.Where(c => c.CheckInSpawnRabge(random)).First();

                        cellEntity.Set<Cell>().Configuration = configuration;

                        hasChain = GameFieldAnalyst.CheckCellInChain(Global.Data.InGame.GameField.Cells, position);
                    }
                }
            }
        }
    }
}
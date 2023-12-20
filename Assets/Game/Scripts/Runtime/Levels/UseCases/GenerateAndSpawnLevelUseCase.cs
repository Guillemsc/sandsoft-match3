using System.Collections.Generic;
using Game.Gems.Datas;
using Game.Gems.UseCases;
using Game.Generation.UseCases;
using Game.Grids.UseCases;
using UnityEngine;

namespace Game.Levels.UseCases
{
    public sealed class GenerateAndSpawnLevelUseCase
    {
        readonly GenerateLevelUseCase _generateLevelUseCase;
        readonly SpawnGemViewUseCase _spawnGemViewUseCase;
        readonly GetWorldPositionFromGridPositionUseCase _getWorldPositionFromGridPositionUseCase;

        public GenerateAndSpawnLevelUseCase(
            GenerateLevelUseCase generateLevelUseCase,
            SpawnGemViewUseCase spawnGemViewUseCase, 
            GetWorldPositionFromGridPositionUseCase getWorldPositionFromGridPositionUseCase
            )
        {
            _generateLevelUseCase = generateLevelUseCase;
            _spawnGemViewUseCase = spawnGemViewUseCase;
            _getWorldPositionFromGridPositionUseCase = getWorldPositionFromGridPositionUseCase;
        }

        public void Execute()
        {
            List<GridGemData> gems = _generateLevelUseCase.Execute(new Vector2Int(8, 8));

            foreach (GridGemData gem in gems)
            {
                Vector2 worldPosition = _getWorldPositionFromGridPositionUseCase.Execute(gem.GridPosition);
                
                _spawnGemViewUseCase.Execute(gem.GemType, worldPosition);
            }
        }
    }
}
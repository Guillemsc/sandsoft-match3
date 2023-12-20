using System.Collections.Generic;
using Game.Gems.Datas;
using Game.Gems.UseCases;
using Game.Generation.UseCases;
using Game.Grids.Configurations;
using Game.Grids.UseCases;
using UnityEngine;

namespace Game.Levels.UseCases
{
    public sealed class GenerateAndSpawnLevelUseCase
    {
        readonly GridsConfiguration _gridsConfiguration;
        readonly GenerateLevelUseCase _generateLevelUseCase;
        readonly SpawnGemViewUseCase _spawnGemViewUseCase;
        readonly GetWorldPositionFromGridPositionUseCase _getWorldPositionFromGridPositionUseCase;

        public GenerateAndSpawnLevelUseCase(
            GridsConfiguration gridsConfiguration,
            GenerateLevelUseCase generateLevelUseCase,
            SpawnGemViewUseCase spawnGemViewUseCase, 
            GetWorldPositionFromGridPositionUseCase getWorldPositionFromGridPositionUseCase
            )
        {
            _gridsConfiguration = gridsConfiguration;
            _generateLevelUseCase = generateLevelUseCase;
            _spawnGemViewUseCase = spawnGemViewUseCase;
            _getWorldPositionFromGridPositionUseCase = getWorldPositionFromGridPositionUseCase;
        }

        public void Execute()
        {
            Dictionary<Vector2Int, GridGemData> gems = _generateLevelUseCase.Execute(
                _gridsConfiguration.GridSize
            );

            foreach (GridGemData gem in gems.Values)
            {
                Vector2 worldPosition = _getWorldPositionFromGridPositionUseCase.Execute(gem.GridPosition);
                
                _spawnGemViewUseCase.Execute(gem.GemType, worldPosition);
            }
        }
    }
}
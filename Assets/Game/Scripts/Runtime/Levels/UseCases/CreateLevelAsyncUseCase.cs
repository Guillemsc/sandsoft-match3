using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Game.Gems.Datas;
using Game.Gems.UseCases;
using Game.Generation.UseCases;
using Game.Grids.Configurations;
using Game.Grids.UseCases;
using Game.Levels.Data;
using GUtils.Extensions;
using GUtils.Tasks.Runners;
using UnityEngine;

namespace Game.Levels.UseCases
{
    public sealed class CreateLevelAsyncUseCase
    {
        readonly LevelData _levelData;
        readonly GridsConfiguration _gridsConfiguration;
        readonly GenerateLevelUseCase _generateLevelUseCase;
        readonly SpawnGemViewUseCase _spawnGemViewUseCase;
        readonly GetWorldPositionFromGridPositionUseCase _getWorldPositionFromGridPositionUseCase;
        readonly PlayLevelShowAnimationAsyncUseCase _playLevelShowAnimationAsyncUseCase;

        public CreateLevelAsyncUseCase(
            LevelData levelData,
            GridsConfiguration gridsConfiguration,
            GenerateLevelUseCase generateLevelUseCase,
            SpawnGemViewUseCase spawnGemViewUseCase, 
            GetWorldPositionFromGridPositionUseCase getWorldPositionFromGridPositionUseCase,
            PlayLevelShowAnimationAsyncUseCase playLevelShowAnimationAsyncUseCase
            )
        {
            _levelData = levelData;
            _gridsConfiguration = gridsConfiguration;
            _generateLevelUseCase = generateLevelUseCase;
            _spawnGemViewUseCase = spawnGemViewUseCase;
            _getWorldPositionFromGridPositionUseCase = getWorldPositionFromGridPositionUseCase;
            _playLevelShowAnimationAsyncUseCase = playLevelShowAnimationAsyncUseCase;
        }

        public async Task Execute(CancellationToken cancellationToken)
        {
            if (_levelData.BusyLoadingOrUnloading)
            {
                return;
            }

            _levelData.BusyLoadingOrUnloading = true;
                
            Dictionary<Vector2Int, GridGemData> gems = _generateLevelUseCase.Execute(
                _gridsConfiguration.GridSize
            );

            foreach (GridGemData gem in gems.Values)
            {
                Vector2 worldPosition = _getWorldPositionFromGridPositionUseCase.Execute(gem.GridPosition);
                
                _spawnGemViewUseCase.Execute(gem.GemType, worldPosition);
            }

            await _playLevelShowAnimationAsyncUseCase.Execute(cancellationToken);
                
            _levelData.BusyLoadingOrUnloading = false;
        }
    }
}
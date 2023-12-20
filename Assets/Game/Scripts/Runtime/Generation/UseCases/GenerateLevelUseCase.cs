using System.Collections.Generic;
using Game.Gems.Datas;
using Game.Gems.Enums;
using UnityEngine;

namespace Game.Generation.UseCases
{
    public sealed class GenerateLevelUseCase
    {
        readonly GenerateNecessaryGemTypesForGridSizeUseCase _generateNecessaryGemTypesForGridSizeUseCase;
        readonly GenerateLevelFromNecessaryGemTypesUseCase _generateLevelFromNecessaryGemTypesUseCase;

        public GenerateLevelUseCase(
            GenerateNecessaryGemTypesForGridSizeUseCase generateNecessaryGemTypesForGridSizeUseCase,
            GenerateLevelFromNecessaryGemTypesUseCase generateLevelFromNecessaryGemTypesUseCase
            )
        {
            _generateNecessaryGemTypesForGridSizeUseCase = generateNecessaryGemTypesForGridSizeUseCase;
            _generateLevelFromNecessaryGemTypesUseCase = generateLevelFromNecessaryGemTypesUseCase;
        }

        public Dictionary<Vector2Int, GridGemData> Execute(Vector2Int gridSize)
        {
            Dictionary<GemType, int> gems = _generateNecessaryGemTypesForGridSizeUseCase.Execute(gridSize);

            Dictionary<Vector2Int, GridGemData> level = _generateLevelFromNecessaryGemTypesUseCase.Execute(
                gridSize,
                ref gems
            );
            
            return level;
        }
    }
}
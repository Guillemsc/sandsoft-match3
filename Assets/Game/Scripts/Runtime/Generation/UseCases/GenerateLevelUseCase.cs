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
        readonly RandomSwapGemsThatAreInvalidUseCase _randomSwapGemsThatAreInvalidUseCase;

        public GenerateLevelUseCase(
            GenerateNecessaryGemTypesForGridSizeUseCase generateNecessaryGemTypesForGridSizeUseCase,
            GenerateLevelFromNecessaryGemTypesUseCase generateLevelFromNecessaryGemTypesUseCase,
            RandomSwapGemsThatAreInvalidUseCase randomSwapGemsThatAreInvalidUseCase
            )
        {
            _generateNecessaryGemTypesForGridSizeUseCase = generateNecessaryGemTypesForGridSizeUseCase;
            _generateLevelFromNecessaryGemTypesUseCase = generateLevelFromNecessaryGemTypesUseCase;
            _randomSwapGemsThatAreInvalidUseCase = randomSwapGemsThatAreInvalidUseCase;
        }

        public Dictionary<Vector2Int, GridGemData> Execute(Vector2Int gridSize)
        {
            Dictionary<GemType, int> gems = _generateNecessaryGemTypesForGridSizeUseCase.Execute(gridSize);

            Dictionary<Vector2Int, GridGemData> level = _generateLevelFromNecessaryGemTypesUseCase.Execute(
                gridSize,
                ref gems
            );
            
            _randomSwapGemsThatAreInvalidUseCase.Execute(ref level);
            
            return level;
        }
    }
}
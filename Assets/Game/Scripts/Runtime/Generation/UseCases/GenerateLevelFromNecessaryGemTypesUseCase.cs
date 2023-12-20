using System.Collections.Generic;
using Game.Gems.Datas;
using Game.Gems.Enums;
using GUtils.Randomization.Extensions;
using GUtils.Randomization.Generators;
using UnityEngine;

namespace Game.Generation.UseCases
{
    public sealed class GenerateLevelFromNecessaryGemTypesUseCase
    {
        readonly IRandomGenerator _randomGenerator;
        readonly GetValidGemTypesForGridPositionUseCase _getValidGemTypesForGridPositionUseCase;
        readonly GetGemTypeForLevelGemsThatStillHaveLeftToPlaceUseCase _getGemTypeForLevelGemsThatStillHaveLeftToPlaceUseCase;

        public GenerateLevelFromNecessaryGemTypesUseCase(
            IRandomGenerator randomGenerator,
            GetValidGemTypesForGridPositionUseCase getValidGemTypesForGridPositionUseCase, 
            GetGemTypeForLevelGemsThatStillHaveLeftToPlaceUseCase getGemTypeForLevelGemsThatStillHaveLeftToPlaceUseCase
            )
        {
            _randomGenerator = randomGenerator;
            _getValidGemTypesForGridPositionUseCase = getValidGemTypesForGridPositionUseCase;
            _getGemTypeForLevelGemsThatStillHaveLeftToPlaceUseCase = getGemTypeForLevelGemsThatStillHaveLeftToPlaceUseCase;
        }

        public Dictionary<Vector2Int, GridGemData> Execute(
            Vector2Int gridSize,
            ref Dictionary<GemType, int> gems
            )
        {
            Dictionary<Vector2Int, GridGemData> level = new();
            
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    Vector2Int gridPosition = new(x, y);

                    GenerateGridGem(ref gems, ref level, gridPosition);
                }
            }

            return level;
        }

        void GenerateGridGem(
            ref Dictionary<GemType, int> gems, 
            ref Dictionary<Vector2Int, GridGemData> level,
            Vector2Int gridPosition
            )
        {
            List<GemType> gemTypes = _getValidGemTypesForGridPositionUseCase.Execute(
                in gems,
                in level,
                gridPosition
            );

            bool foundRandom = _randomGenerator.TryGetRandom(gemTypes, out GemType gemType);

            if (!foundRandom)
            {
                gemType = _getGemTypeForLevelGemsThatStillHaveLeftToPlaceUseCase.Execute(ref gems);
            }

            GridGemData gridGemData = new(gridPosition, gemType);
            
            level.Add(gridGemData.GridPosition, gridGemData);

            gems[gemType] -= 1;
        }
    }
}
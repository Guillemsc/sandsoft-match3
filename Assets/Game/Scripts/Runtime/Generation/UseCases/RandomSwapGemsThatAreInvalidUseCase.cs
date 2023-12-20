using System.Collections.Generic;
using System.Linq;
using Game.Gems.Datas;
using GUtils.Randomization.Extensions;
using GUtils.Randomization.Generators;
using UnityEngine;

namespace Game.Generation.UseCases
{
    public sealed class RandomSwapGemsThatAreInvalidUseCase
    {
        static readonly List<Vector2Int> OffsetsToCheck = new()
        {
            new Vector2Int(-1, 0),
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(0, 1),
        };

        readonly IRandomGenerator _randomGenerator;
        readonly SwapLevelGemsUseCase _swapLevelGemsUseCase;

        public RandomSwapGemsThatAreInvalidUseCase(
            IRandomGenerator randomGenerator,
            SwapLevelGemsUseCase swapLevelGemsUseCase
            )
        {
            _randomGenerator = randomGenerator;
            _swapLevelGemsUseCase = swapLevelGemsUseCase;
        }

        public void Execute(ref Dictionary<Vector2Int, GridGemData> level)
        {
            List<GridGemData> gems = level.Values.ToList();
            
            foreach (GridGemData gem in gems)
            {
                int sameGemTouchingCount = 0;
                
                foreach (Vector2Int offset in OffsetsToCheck)
                {
                    Vector2Int checkingPosition = gem.GridPosition + offset;

                    bool hasGem = level.TryGetValue(checkingPosition, out GridGemData gridGemData);

                    if (!hasGem)
                    {
                        continue;
                    }

                    bool isSameType = gridGemData.GemType == gem.GemType;

                    if (isSameType)
                    {
                        ++sameGemTouchingCount;
                    }
                }

                if (sameGemTouchingCount < 2)
                {
                    continue;
                }

                bool randomGemFound = _randomGenerator.TryGetRandomValue(level, out GridGemData randomGem);

                if (randomGemFound)
                {
                    _swapLevelGemsUseCase.Execute(ref level, gem, randomGem);
                }
            }
        }
    }
}
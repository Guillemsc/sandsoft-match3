using System.Collections.Generic;
using Game.Gems.Datas;
using Game.Gems.Enums;
using UnityEngine;

namespace Game.Generation.UseCases
{
    public sealed class GetValidGemTypesForGridPositionUseCase
    {
        static readonly List<Vector2Int> OffsetsToCheck = new()
        {
            new Vector2Int(-1, 0),
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(0, 1),
        };
        
        public List<GemType> Execute(
            in Dictionary<GemType, int> gems,
            in Dictionary<Vector2Int, GridGemData> level,
            Vector2Int gridPosition
        )
        {
            List<GemType> gemTypesToCheck = new();
            
            foreach (KeyValuePair<GemType, int> gem in gems)
            {
                bool hasLeft = gem.Value > 0;
                
                if (hasLeft)
                {
                    gemTypesToCheck.Add(gem.Key);   
                }
            }

            List<GemType> validTypes = new();
            
            foreach (GemType gemType in gemTypesToCheck)
            {
                int sameGemTouchingCount = 0;
                
                foreach (Vector2Int offset in OffsetsToCheck)
                {
                    Vector2Int checkingPosition = gridPosition + offset;

                    bool hasGem = level.TryGetValue(checkingPosition, out GridGemData gridGemData);

                    if (!hasGem)
                    {
                        continue;
                    }

                    bool isSameType = gridGemData.GemType == gemType;

                    if (isSameType)
                    {
                        ++sameGemTouchingCount;
                    }
                }

                if (sameGemTouchingCount < 2)
                {
                    validTypes.Add(gemType);
                }
            }

            return validTypes;
        }
    }
}
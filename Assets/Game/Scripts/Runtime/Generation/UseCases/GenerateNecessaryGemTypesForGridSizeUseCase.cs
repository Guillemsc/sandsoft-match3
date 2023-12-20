using System.Collections.Generic;
using Game.Gems.Enums;
using GUtils.Randomization.Extensions;
using GUtils.Randomization.Generators;
using UnityEngine;

namespace Game.Generation.UseCases
{
    public sealed class GenerateNecessaryGemTypesForGridSizeUseCase
    {
        readonly IRandomGenerator _randomGenerator;

        public GenerateNecessaryGemTypesForGridSizeUseCase(
            IRandomGenerator randomGenerator
            )
        {
            _randomGenerator = randomGenerator;
        }

        public Dictionary<GemType, int> Execute(Vector2Int gridSize)
        {
            Dictionary<GemType, int> ret = new();

            int slots = gridSize.x * gridSize.y;

            int usedSlots = 0;

            while (usedSlots < slots)
            {
                GemType gemType = _randomGenerator.UnsafeGetRandom<GemType>();

                int slotsLeft = slots - usedSlots;
                
                bool canUse3Slots = slotsLeft >= 3;
                
                int gemUsedSlots;

                if (canUse3Slots)
                {
                    gemUsedSlots = 3;
                }
                else
                {
                    bool shouldPickFromAlreadyExistingTypes = !ret.ContainsKey(gemType);
                    
                    if (shouldPickFromAlreadyExistingTypes)
                    {
                        _randomGenerator.TryGetRandomKey(ret, out gemType);   
                    }
                    
                    gemUsedSlots = slotsLeft;
                }

                usedSlots += gemUsedSlots;

                bool typeAlreadyAdded = ret.ContainsKey(gemType);
                
                if (!typeAlreadyAdded)
                {
                    ret.Add(gemType, 0);
                }

                ret[gemType] += gemUsedSlots;
            }

            return ret;
        }
    }
}
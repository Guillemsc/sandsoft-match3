using System.Collections.Generic;
using Game.Gems.Enums;

namespace Game.Generation.UseCases
{
    public sealed class GetGemTypeForLevelGemsThatStillHaveLeftToPlaceUseCase
    {
        public GemType Execute(
            ref Dictionary<GemType, int> gems
            )
        {
            foreach (KeyValuePair<GemType, int> gem in gems)
            {
                bool hasPriority = gem.Value is > 0 and < 3;
                
                if (hasPriority)
                {
                    return gem.Key;
                }
            }
            
            foreach (KeyValuePair<GemType, int> gem in gems)
            {
                bool hasLeft = gem.Value > 0;
                
                if (hasLeft)
                {
                    return gem.Key;
                }
            }
            
            return GemType.Apple;
        }
    }
}
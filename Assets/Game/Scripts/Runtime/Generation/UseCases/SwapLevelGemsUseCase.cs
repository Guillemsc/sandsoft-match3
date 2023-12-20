using System.Collections.Generic;
using Game.Gems.Datas;
using UnityEngine;

namespace Game.Generation.UseCases
{
    public sealed class SwapLevelGemsUseCase
    {
        public void Execute(
            ref Dictionary<Vector2Int, GridGemData> level,
            GridGemData gem1,
            GridGemData gem2
        )
        {
            level[gem1.GridPosition] = gem2;
            level[gem2.GridPosition] = gem1;
            
            (gem2.GridPosition, gem1.GridPosition) = (gem1.GridPosition, gem2.GridPosition);
        }
    }
}
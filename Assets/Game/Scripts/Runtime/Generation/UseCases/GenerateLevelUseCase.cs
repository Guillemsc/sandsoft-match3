using System.Collections.Generic;
using Game.Gems.Datas;
using Game.Gems.Enums;
using UnityEngine;

namespace Game.Generation.UseCases
{
    public sealed class GenerateLevelUseCase
    {
        public List<GridGemData> Execute(Vector2Int gridSize)
        {
            List<GridGemData> ret = new();

            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    ret.Add(new GridGemData(new Vector2Int(x, y), GemType.Apple));
                }
            }

            return ret;
        }
    }
}
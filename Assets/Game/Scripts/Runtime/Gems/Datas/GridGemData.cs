using Game.Gems.Enums;
using UnityEngine;

namespace Game.Gems.Datas
{
    public sealed class GridGemData
    {
        public Vector2Int GridPosition { get; set; }
        public GemType GemType { get; }
        
        public GridGemData(Vector2Int gridPosition, GemType gemType)
        {
            GridPosition = gridPosition;
            GemType = gemType;
        }
    }
}
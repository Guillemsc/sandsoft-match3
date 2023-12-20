using System.Collections.Generic;
using Game.Gems.Enums;
using GUtils.Directions;
using UnityEngine;

namespace Game.Generation.Datas
{
    public sealed class SolvedGemLineData
    {
        public GemType GemType { get; }
        public List<Vector2Int> GridPositions { get; }
        public Orientation2 Orientation { get; }
        
        public SolvedGemLineData(GemType gemType, List<Vector2Int> gridPositions, Orientation2 orientation)
        {
            GemType = gemType;
            GridPositions = gridPositions;
            Orientation = orientation;
        }
    }
}
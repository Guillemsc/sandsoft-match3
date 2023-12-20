using UnityEngine;

namespace Game.Grids.Configurations
{
    [CreateAssetMenu(fileName = "GridsConfiguration", menuName = "Game/Configuration/GridsConfiguration", order = 1)]
    public sealed class GridsConfiguration : ScriptableObject
    {
        [SerializeField] [Min(0f)] public float TileSize = 1f;
        [SerializeField] public Vector2Int GridSize = new(8, 8);
    }
}
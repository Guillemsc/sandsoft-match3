using Game.Grids.Configurations;
using UnityEngine;

namespace Game.Grids.UseCases
{
    public sealed class GetWorldPositionFromGridPositionUseCase
    {
        readonly GridsConfiguration _gridsConfiguration;

        public GetWorldPositionFromGridPositionUseCase(
            GridsConfiguration gridsConfiguration
            )
        {
            _gridsConfiguration = gridsConfiguration;
        }

        public Vector2 Execute(Vector2Int gridPosition)
        {
            return new Vector2(
                gridPosition.x * _gridsConfiguration.TileSize,
                gridPosition.y * _gridsConfiguration.TileSize
            );
        }
    }
}
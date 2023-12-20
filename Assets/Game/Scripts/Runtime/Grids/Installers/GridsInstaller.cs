using Game.Grids.Configurations;
using Game.Grids.UseCases;
using GUtils.Di.Builder;

namespace Game.Grids.Installers
{
    public static class GridsInstaller
    {
        public static void InstallGrids(this IDiContainerBuilder builder)
        {
            builder.Bind<GetWorldPositionFromGridPositionUseCase>()
                .FromFunction(c => new GetWorldPositionFromGridPositionUseCase(
                    c.Resolve<GridsConfiguration>()
                ));
        }
    }
}
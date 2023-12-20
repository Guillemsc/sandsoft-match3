using Game.Gems.UseCases;
using Game.Generation.UseCases;
using Game.Grids.UseCases;
using Game.Levels.UseCases;
using GUtils.Di.Builder;

namespace Game.Levels.Installers
{
    public static class LevelsInstaller
    {
        public static void InstallLevels(this IDiContainerBuilder builder)
        {
            builder.Bind<GenerateAndSpawnLevelUseCase>()
                .FromFunction(c => new GenerateAndSpawnLevelUseCase(
                    c.Resolve<GenerateLevelUseCase>(),
                    c.Resolve<SpawnGemViewUseCase>(),
                    c.Resolve<GetWorldPositionFromGridPositionUseCase>()
                ));
        }
    }
}
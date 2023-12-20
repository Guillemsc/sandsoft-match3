using Game.Gems.Datas;
using Game.Gems.UseCases;
using Game.Generation.UseCases;
using Game.Grids.Configurations;
using Game.Grids.UseCases;
using Game.Levels.Data;
using Game.Levels.UseCases;
using GUtils.Di.Builder;
using GUtils.Tasks.Runners;

namespace Game.Levels.Installers
{
    public static class LevelsInstaller
    {
        public static void InstallLevels(this IDiContainerBuilder builder)
        {
            builder.Bind<LevelData>().FromNew();
            
            builder.Bind<CreateLevelAsyncUseCase>()
                .FromFunction(c => new CreateLevelAsyncUseCase(
                    c.Resolve<LevelData>(),
                    c.Resolve<GridsConfiguration>(),
                    c.Resolve<GenerateLevelUseCase>(),
                    c.Resolve<SpawnGemViewUseCase>(),
                    c.Resolve<GetWorldPositionFromGridPositionUseCase>(),
                    c.Resolve<PlayLevelShowAnimationAsyncUseCase>()
                ));

            builder.Bind<ClearLevelAsyncUseCase>()
                .FromFunction(c => new ClearLevelAsyncUseCase(
                    c.Resolve<LevelData>(),
                    c.Resolve<DespawnAllGemViewsUseCase>(),
                    c.Resolve<PlayLevelHideAnimationAsyncUseCase>()
                ));

            builder.Bind<RegenerateLevelUseCase>()
                .FromFunction(c => new RegenerateLevelUseCase(
                    c.Resolve<IAsyncTaskRunner>(),
                    c.Resolve<CreateLevelAsyncUseCase>(),
                    c.Resolve<ClearLevelAsyncUseCase>()
                ));

            builder.Bind<PlayLevelShowAnimationAsyncUseCase>()
                .FromFunction(c => new PlayLevelShowAnimationAsyncUseCase(
                    c.Resolve<GemViewsData>()
                ));

            builder.Bind<PlayLevelHideAnimationAsyncUseCase>()
                .FromFunction(c => new PlayLevelHideAnimationAsyncUseCase(
                    c.Resolve<GemViewsData>()
                ));
        }
    }
}
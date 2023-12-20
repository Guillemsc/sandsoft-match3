using Game.Gems.Configurations;
using Game.Gems.Datas;
using Game.Gems.UseCases;
using Game.General.Datas;
using GUtils.Di.Builder;

namespace Game.Gems.Installers
{
    public static class GemsInstaller
    {
        public static void InstallGems(this IDiContainerBuilder builder)
        {
            builder.Bind<GemViewsData>().FromNew();
            
            builder.Bind<GetGemConfigurationByGemTypeUseCase>()
                .FromFunction(c => new GetGemConfigurationByGemTypeUseCase(
                    c.Resolve<GemsConfiguration>()
                ));
            
            builder.Bind<SpawnGemViewUseCase>()
                .FromFunction(c => new SpawnGemViewUseCase(
                    c.Resolve<GemsConfiguration>(),
                    c.Resolve<GeneralSceneData>(),
                    c.Resolve<GemViewsData>(),
                    c.Resolve<GetGemConfigurationByGemTypeUseCase>()
                ));

            builder.Bind<DespawnAllGemViewsUseCase>()
                .FromFunction(c => new DespawnAllGemViewsUseCase(
                    c.Resolve<GemViewsData>()
                ));
        }
    }
}
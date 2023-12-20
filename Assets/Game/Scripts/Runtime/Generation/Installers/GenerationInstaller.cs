using Game.Generation.UseCases;
using GUtils.Di.Builder;

namespace Game.Generation.Installers
{
    public static class GenerationInstaller
    {
        public static void InstallGeneration(this IDiContainerBuilder builder)
        {
            builder.Bind<GenerateLevelUseCase>()
                .FromFunction(c => new GenerateLevelUseCase(
                ));
        }
    }
}
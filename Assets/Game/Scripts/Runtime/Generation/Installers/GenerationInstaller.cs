using Game.Generation.UseCases;
using GUtils.Di.Builder;
using GUtils.Randomization.Generators;
using GUtilsUnity.Randomization.Generators;

namespace Game.Generation.Installers
{
    public static class GenerationInstaller
    {
        public static void InstallGeneration(this IDiContainerBuilder builder)
        {
            builder.Bind<IRandomGenerator>()
                .FromInstance(UnityRandomGenerator.Instance);
            
            builder.Bind<GenerateLevelUseCase>()
                .FromFunction(c => new GenerateLevelUseCase(
                ));
        }
    }
}
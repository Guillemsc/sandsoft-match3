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
                .FromInstance(new SeedRandomGenerator(1));
            
            builder.Bind<GenerateLevelUseCase>()
                .FromFunction(c => new GenerateLevelUseCase(
                    c.Resolve<GenerateNecessaryGemTypesForGridSizeUseCase>(),
                    c.Resolve<GenerateLevelFromNecessaryGemTypesUseCase>()
                ));

            builder.Bind<GenerateNecessaryGemTypesForGridSizeUseCase>()
                .FromFunction(c => new GenerateNecessaryGemTypesForGridSizeUseCase(
                    c.Resolve<IRandomGenerator>()
                ));

            builder.Bind<GenerateLevelFromNecessaryGemTypesUseCase>()
                .FromFunction(c => new GenerateLevelFromNecessaryGemTypesUseCase(
                    c.Resolve<IRandomGenerator>(),
                    c.Resolve<GetValidGemTypesForGridPositionUseCase>(),
                    c.Resolve<GetGemTypeForLevelGemsThatStillHaveLeftToPlaceUseCase>()
                ));

            builder.Bind<GetValidGemTypesForGridPositionUseCase>()
                .FromFunction(c => new GetValidGemTypesForGridPositionUseCase());

            builder.Bind<GetGemTypeForLevelGemsThatStillHaveLeftToPlaceUseCase>()
                .FromFunction(c => new GetGemTypeForLevelGemsThatStillHaveLeftToPlaceUseCase());
        }
    }
}
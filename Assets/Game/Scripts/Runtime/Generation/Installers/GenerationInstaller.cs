using Game.Generation.UseCases;
using GUtils.Di.Builder;
using GUtils.Randomization.Generators;

namespace Game.Generation.Installers
{
    public static class GenerationInstaller
    {
        public static void InstallGeneration(this IDiContainerBuilder builder)
        {
            builder.Bind<GenerateLevelUseCase>()
                .FromFunction(c => new GenerateLevelUseCase(
                    c.Resolve<GenerateNecessaryGemTypesForGridSizeUseCase>(),
                    c.Resolve<GenerateLevelFromNecessaryGemTypesUseCase>(),
                    c.Resolve<RandomSwapGemsThatAreInvalidUseCase>()
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

            builder.Bind<SwapLevelGemsUseCase>()
                .FromFunction(c => new SwapLevelGemsUseCase());

            builder.Bind<RandomSwapGemsThatAreInvalidUseCase>()
                .FromFunction(c => new RandomSwapGemsThatAreInvalidUseCase(
                    c.Resolve<IRandomGenerator>(),
                    c.Resolve<SwapLevelGemsUseCase>()
                ));
        }
    }
}
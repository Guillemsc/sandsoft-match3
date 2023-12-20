using Game.General.Interactors;
using Game.General.UseCases;
using Game.Levels.UseCases;
using GUtils.Di.Builder;

namespace Game.General.Installers
{
    public static class GeneralInstaller
    {
        public static void InstallGeneral(this IDiContainerBuilder builder)
        {
            builder.Bind<IGameInteractor>()
                .FromFunction(c => new GameInteractor(
                    c.Resolve<GameStartUseCase>()
                ));

            builder.Bind<GameStartUseCase>()
                .FromFunction(c => new GameStartUseCase(
                    c.Resolve<GenerateAndSpawnLevelUseCase>()
                ));
        }
    }
}
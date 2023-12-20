using Game.General.Interactors;
using GUtils.Di.Builder;

namespace Game.General.Installers
{
    public static class GeneralInstaller
    {
        public static void InstallGeneral(this IDiContainerBuilder builder)
        {
            builder.Bind<IGameInteractor>()
                .FromFunction(c => new GameInteractor(
                ));
        }
    }
}
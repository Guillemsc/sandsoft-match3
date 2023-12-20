using Game.Gems.Configurations;
using Game.General.Configurations;
using Game.Grids.Configurations;
using GUtils.Di.Builder;

namespace Game.General.Installers
{
    public static class GeneralConfigurationInstaller
    {
        public static void InstallGeneralConfiguration(this IDiContainerBuilder builder, GeneralConfiguration generalConfiguration)
        {
            builder.Bind<GeneralConfiguration>().FromInstance(generalConfiguration);
            builder.Bind<GemsConfiguration>().FromInstance(generalConfiguration.GemsConfiguration);
            builder.Bind<GridsConfiguration>().FromInstance(generalConfiguration.GridsConfiguration);
        }
    }
}
using GUtils.Di.Builder;

namespace GUtils.Di.Installers
{
    public interface IInstaller
    {
        void Install(IDiContainerBuilder builder);
    }
}

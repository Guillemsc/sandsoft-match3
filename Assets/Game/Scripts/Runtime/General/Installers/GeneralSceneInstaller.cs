using Game.General.Datas;
using GUtils.Di.Builder;
using GUtils.Di.Installers;
using UnityEngine;

namespace Game.General.Installers
{
    public sealed class GeneralSceneInstaller : MonoBehaviour, IInstaller
    {
        public GameObject Root;
        
        public void Install(IDiContainerBuilder builder)
        {
            builder.Bind<GeneralSceneData>()
                .FromInstance(new GeneralSceneData(
                    Root
                ));
        }
    }
}
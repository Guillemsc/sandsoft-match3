using Game.General.Datas;
using Game.RegenerateUi.Installers;
using GUtils.Di.Builder;
using GUtils.Di.Installers;
using UnityEngine;

namespace Game.General.Installers
{
    public sealed class GeneralSceneInstaller : MonoBehaviour, IInstaller
    {
        [Header("Misc")]
        [SerializeField] public GameObject Root;
        
        [Header("Ui")] 
        [SerializeField] public RegenerateUiInstaller RegenerateUiInstaller;
        
        public void Install(IDiContainerBuilder builder)
        {
            builder.Bind<GeneralSceneData>()
                .FromInstance(new GeneralSceneData(
                    Root
                ));
            
            builder.Install(RegenerateUiInstaller);
        }
    }
}
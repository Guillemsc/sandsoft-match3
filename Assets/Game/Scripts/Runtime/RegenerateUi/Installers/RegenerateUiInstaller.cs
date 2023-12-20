using Game.Levels.UseCases;
using Game.RegenerateUi.UseCases;
using GUtils.Di.Builder;
using GUtils.Di.Installers;
using GUtilsUnity.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Game.RegenerateUi.Installers
{
    public sealed class RegenerateUiInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] public Button RegenerateButton;
        
        public void Install(IDiContainerBuilder builder)
        {
            builder.Bind<WhenRegenerateButtonPressedUseCase>()
                .FromFunction(c => new WhenRegenerateButtonPressedUseCase(
                    c.Resolve<ClearLevelUseCase>(),
                    c.Resolve<GenerateAndSpawnLevelUseCase>()
                ))
                .LinkButtonClick(RegenerateButton, o => o.Execute);
        }
    }
}
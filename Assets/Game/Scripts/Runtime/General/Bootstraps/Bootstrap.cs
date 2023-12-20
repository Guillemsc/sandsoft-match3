using Game.Gems.Installers;
using Game.General.Configurations;
using Game.General.Grids.Installers;
using Game.General.Installers;
using Game.General.Interactors;
using GUtils.Di.Contexts;
using GUtils.Di.Installers;
using GUtils.Disposing.Disposables;
using UnityEngine;

namespace Game.General.Bootstraps
{
    public sealed class Bootstrap : MonoBehaviour
    {
        [SerializeField] GeneralSceneInstaller SceneInstaller;
        [SerializeField] GeneralConfiguration Configuration;
        
        void Awake()
        {
            // We create a Dependency Injection context. 
            // This is just a fancy name for an object that's going to help us install a bunch
            // of different stuff.
            // The IGameInteractor is the final object that the DiContext will return us 
            // once everything is installed (it's installed on the GeneralInstaller)
            DiContext<IGameInteractor> diContext = new DiContext<IGameInteractor>();
            
            // We install the SceneInstaller, which contains references we need from the scene.
            diContext.AddInstaller(SceneInstaller);
            
            // We install the logic of our game. The game is divided in several installer,
            // to keep things organised.
            diContext.AddInstaller(new CallbackInstaller(b =>
            {
                b.InstallGeneralConfiguration(Configuration);
                b.InstallGeneral();
                b.InstallGems();
                b.InstallGrids();
            }));

            // Here we resolve our installation. This means that each object is going to be created.
            IDisposable<IGameInteractor> gameInteractor = diContext.Install();
            
            // We start our game.
            gameInteractor.Value.Start();
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using Game.Gems.UseCases;
using Game.Levels.Data;
using GUtils.Tasks.Runners;

namespace Game.Levels.UseCases
{
    public sealed class ClearLevelAsyncUseCase
    {
        readonly LevelData _levelData;
        readonly DespawnAllGemViewsUseCase _despawnAllGemViewsUseCase;
        readonly PlayLevelHideAnimationAsyncUseCase _playLevelHideAnimationAsyncUseCase;

        public ClearLevelAsyncUseCase(
            LevelData levelData,
            DespawnAllGemViewsUseCase despawnAllGemViewsUseCase,
            PlayLevelHideAnimationAsyncUseCase playLevelHideAnimationAsyncUseCase
            )
        {
            _levelData = levelData;
            _despawnAllGemViewsUseCase = despawnAllGemViewsUseCase;
            _playLevelHideAnimationAsyncUseCase = playLevelHideAnimationAsyncUseCase;
        }

        public async Task Execute(CancellationToken cancellationToken)
        {
            if (_levelData.BusyLoadingOrUnloading)
            {
                return;
            }
            
            _levelData.BusyLoadingOrUnloading = true;
                
            await _playLevelHideAnimationAsyncUseCase.Execute(cancellationToken);
                
            if(cancellationToken.IsCancellationRequested) return;
                
            _despawnAllGemViewsUseCase.Execute();
                
            _levelData.BusyLoadingOrUnloading = false;
        }
    }
}
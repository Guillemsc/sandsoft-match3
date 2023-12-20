using Game.Gems.UseCases;

namespace Game.Levels.UseCases
{
    public sealed class ClearLevelUseCase
    {
        readonly DespawnAllGemViewsUseCase _despawnAllGemViewsUseCase;

        public ClearLevelUseCase(DespawnAllGemViewsUseCase despawnAllGemViewsUseCase)
        {
            _despawnAllGemViewsUseCase = despawnAllGemViewsUseCase;
        }

        public void Execute()
        {
            _despawnAllGemViewsUseCase.Execute();
        }
    }
}
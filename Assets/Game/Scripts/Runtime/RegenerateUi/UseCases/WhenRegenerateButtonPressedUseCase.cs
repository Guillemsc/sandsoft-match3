using Game.Levels.UseCases;

namespace Game.RegenerateUi.UseCases
{
    public sealed class WhenRegenerateButtonPressedUseCase
    {
        readonly RegenerateLevelUseCase _regenerateLevelUseCase;

        public WhenRegenerateButtonPressedUseCase(
            RegenerateLevelUseCase regenerateLevelUseCase
            )
        {
            _regenerateLevelUseCase = regenerateLevelUseCase;
        }

        public void Execute()
        {
            _regenerateLevelUseCase.Execute();   
        }
    }
}
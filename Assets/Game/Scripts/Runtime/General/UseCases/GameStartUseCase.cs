using Game.Levels.UseCases;

namespace Game.General.UseCases
{
    public sealed class GameStartUseCase
    {
        readonly RegenerateLevelUseCase _regenerateLevelUseCase;

        public GameStartUseCase(
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
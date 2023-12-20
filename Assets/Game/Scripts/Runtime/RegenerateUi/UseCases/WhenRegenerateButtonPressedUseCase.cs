using Game.Levels.UseCases;

namespace Game.RegenerateUi.UseCases
{
    public sealed class WhenRegenerateButtonPressedUseCase
    {
        readonly ClearLevelUseCase _clearLevelUseCase;
        readonly GenerateAndSpawnLevelUseCase _generateAndSpawnLevelUseCase;

        public WhenRegenerateButtonPressedUseCase(
            ClearLevelUseCase clearLevelUseCase, 
            GenerateAndSpawnLevelUseCase generateAndSpawnLevelUseCase
            )
        {
            _clearLevelUseCase = clearLevelUseCase;
            _generateAndSpawnLevelUseCase = generateAndSpawnLevelUseCase;
        }

        public void Execute()
        {
            _clearLevelUseCase.Execute();   
            _generateAndSpawnLevelUseCase.Execute();
        }
    }
}
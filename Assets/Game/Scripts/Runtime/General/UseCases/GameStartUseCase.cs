using Game.Levels.UseCases;

namespace Game.General.UseCases
{
    public sealed class GameStartUseCase
    {
        readonly GenerateAndSpawnLevelUseCase _generateAndSpawnLevelUseCase;

        public GameStartUseCase(
            GenerateAndSpawnLevelUseCase generateAndSpawnLevelUseCase
            )
        {
            _generateAndSpawnLevelUseCase = generateAndSpawnLevelUseCase;
        }

        public void Execute()
        {
            _generateAndSpawnLevelUseCase.Execute();
        }
    }
}
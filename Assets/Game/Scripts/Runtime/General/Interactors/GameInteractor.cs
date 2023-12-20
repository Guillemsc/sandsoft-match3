using Game.General.UseCases;

namespace Game.General.Interactors
{
    public sealed class GameInteractor : IGameInteractor
    {
        readonly GameStartUseCase _gameStartUseCase;

        public GameInteractor(
            GameStartUseCase gameStartUseCase
            )
        {
            _gameStartUseCase = gameStartUseCase;
        }

        public void Start()
            => _gameStartUseCase.Execute();
    }
}
using System.Threading;
using System.Threading.Tasks;
using GUtils.Tasks.Runners;

namespace Game.Levels.UseCases
{
    public sealed class RegenerateLevelUseCase
    {
        readonly IAsyncTaskRunner _asyncTaskRunner;
        readonly CreateLevelAsyncUseCase _createLevelAsyncUseCase;
        readonly ClearLevelAsyncUseCase _clearLevelAsyncUseCase;

        public RegenerateLevelUseCase(
            IAsyncTaskRunner asyncTaskRunner, 
            CreateLevelAsyncUseCase createLevelAsyncUseCase, 
            ClearLevelAsyncUseCase clearLevelAsyncUseCase
            )
        {
            _asyncTaskRunner = asyncTaskRunner;
            _createLevelAsyncUseCase = createLevelAsyncUseCase;
            _clearLevelAsyncUseCase = clearLevelAsyncUseCase;
        }

        public void Execute()
        {
            async Task Play(CancellationToken cancellationToken)
            {
                await _clearLevelAsyncUseCase.Execute(cancellationToken);
                await _createLevelAsyncUseCase.Execute(cancellationToken);
            }

            _asyncTaskRunner.Run(Play);
        }
    }
}
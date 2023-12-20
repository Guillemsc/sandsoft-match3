using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using GUtils.Extensions;

namespace GUtilsUnity.Extensions
{
    public static class TweenExtensions
    {
        public static async Task AsyncWaitForCompletion(this Tween tween)
        {
            if (!tween.active)
            {
                return;
            }

            while (tween.active && !tween.IsComplete())
            {
                await Task.Yield();
            }
        }

        public static async Task AsyncWaitForKill(this Tween t)
        {
            if (!t.active)
            {
                return;
            }

            while (t.active)
            {
                await Task.Yield();
            }
        }

        /// <summary>
        /// Plays the tween.
        /// </summary>
        /// <param name="instantly">If instantly is set to true, the tween will be completed instantly</param>
        public static void Play(this Tween tween, bool instantly)
        {
            tween.Play();

            if (instantly)
            {
                tween.Complete(withCallbacks: true);
            }
        }

        /// <summary>
        /// Plays the tween and awaits until it's completed or killed.
        /// </summary>
        /// <param name="instantly">If instantly is set to true, the tween will be completed instantly</param>
        /// <param name="cancellationToken">If cancellation is requested, the tween will be killed</param>
        public static Task PlayAsync(this Tween tween, bool instantly, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) return Task.CompletedTask;

            tween.Play(instantly);

            if (!tween.IsActive() || !tween.IsPlaying())
            {
                return Task.CompletedTask;
            }

            cancellationToken.Register(() => tween.Kill());

            return tween.AwaitCompletitionOrKill(cancellationToken);
        }


        /// <summary>
        /// Plays the tween and awaits until it's completed or killed.
        /// </summary>
        /// <param name="cancellationToken">If cancellation is requested, tween will be killed</param>
        public static Task PlayAsync(this Tween tween, CancellationToken cancellationToken)
        {
            return tween.PlayAsync(instantly: false, cancellationToken);
        }

        /// <summary>
        /// Awaits until the tween is completed or killed.
        /// </summary>
        /// <param name="cancellationToken">If cancellation is requested, we stop waiting and exit immediately</param>
        public static Task AwaitCompletitionOrKill(this Tween tween, CancellationToken cancellationToken)
        {
            return Task.WhenAny(
                cancellationToken.AwaitCancellationRequested(),
                tween.AsyncWaitForCompletion(),
                tween.AsyncWaitForKill()
            );
        }

        /// <summary>
        /// Set the tween to loop infinitely. Has no effect if it is already started
        /// </summary>
        public static T SetLoopInfinitely<T>(this T tween) where T : Tween
        {
            return tween.SetLoops(-1);
        }

        /// <summary>
        /// Set the tween to loop infinitely. Has no effect if it is already started
        /// </summary>
        /// <param name="tween">The Tween instance that this method extends.</param>
        /// <param name="loopType">Loop behaviour type</param>
        public static T SetLoopInfinitely<T>(this T tween, LoopType loopType) where T : Tween
        {
            return tween.SetLoops(-1, loopType);
        }

        /// <summary>
        /// Sets int.MaxValue loops to the tween.
        /// </summary>
        public static T SetMaxLoops<T>(this T tween) where T : Tween
        {
            return tween.SetLoops(int.MaxValue);
        }

        /// <summary>
        /// Sets int.MaxValue loops to the tween.
        /// </summary>
        /// <param name="tween">The Tween instance that this method extends.</param>
        /// <param name="loopType">Loop behaviour type</param>
        public static T SetMaxLoops<T>(this T tween, LoopType loopType) where T : Tween
        {
            return tween.SetLoops(int.MaxValue, loopType);
        }
    }
}

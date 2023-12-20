using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using Game.Gems.Datas;
using Game.Gems.Views;
using GUtilsUnity.Extensions;
using UnityEngine;

namespace Game.Levels.UseCases
{
    public sealed class PlayLevelHideAnimationAsyncUseCase
    {
        readonly GemViewsData _gemViewsData;

        public PlayLevelHideAnimationAsyncUseCase(GemViewsData gemViewsData)
        {
            _gemViewsData = gemViewsData;
        }

        public Task Execute(CancellationToken cancellationToken)
        {
            const float delayBetweenGem = 0.005f;
            
            Sequence sequence = DOTween.Sequence();
            
            for (int i = 0; i < _gemViewsData.GemViews.Count; i++)
            {
                GemView gemView = _gemViewsData.GemViews[i];
                
                float delay = delayBetweenGem * i;
                
                Sequence gemSequence = DOTween.Sequence();
                
                gemSequence.AppendInterval(delay);

                gemSequence.Append(
                    gemView.transform.DOScale(Vector3.zero, 0.2f)
                        .SetEase(Ease.OutQuad)
                );

                sequence.Join(gemSequence);
            }
            
            sequence.AppendCallback(() =>
            {
                foreach (GemView gemView in _gemViewsData.GemViews)
                {
                    gemView.gameObject.SetActive(false);
                }
            });

            return sequence.PlayAsync(cancellationToken);
        }
    }
}
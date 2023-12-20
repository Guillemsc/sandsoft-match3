using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using Game.Gems.Datas;
using Game.Gems.Views;
using GUtilsUnity.Extensions;
using UnityEngine;

namespace Game.Levels.UseCases
{
    public sealed class PlayLevelShowAnimationAsyncUseCase
    {
        readonly GemViewsData _gemViewsData;

        public PlayLevelShowAnimationAsyncUseCase(GemViewsData gemViewsData)
        {
            _gemViewsData = gemViewsData;
        }

        public Task Execute(CancellationToken cancellationToken)
        {
            const float delayBetweenGem = 0.005f;
            
            Sequence sequence = DOTween.Sequence();

            sequence.AppendCallback(() =>
            {
                foreach (GemView gemView in _gemViewsData.GemViews)
                {
                    Transform transform  = gemView.transform;
                    
                    transform.localRotation = Quaternion.Euler(0, 0, 60);
                    transform.localScale = Vector2.zero;
                    
                    gemView.gameObject.SetActive(true);
                }
            });

            for (int i = 0; i < _gemViewsData.GemViews.Count; i++)
            {
                GemView gemView = _gemViewsData.GemViews[i];
                
                float delay = delayBetweenGem * i;
                
                Sequence gemSequence = DOTween.Sequence();
                
                gemSequence.AppendInterval(delay);

                gemSequence.Append(
                    gemView.transform.DOLocalRotate(Vector3.zero, 0.2f)
                );
                gemSequence.Join(
                    gemView.transform.DOScale(Vector3.one, 0.2f)
                        .SetEase(Ease.OutQuad)
                );

                sequence.Join(gemSequence);
            }

            return sequence.PlayAsync(cancellationToken);
        }
    }
}
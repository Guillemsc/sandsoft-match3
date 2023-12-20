using Game.Gems.Datas;
using Game.Gems.Views;
using UnityEngine;

namespace Game.Gems.UseCases
{
    public sealed class DespawnAllGemViewsUseCase
    {
        readonly GemViewsData _gemViewsData;

        public DespawnAllGemViewsUseCase(
            GemViewsData gemViewsData
            )
        {
            _gemViewsData = gemViewsData;
        }

        public void Execute()
        {
            foreach (GemView gemViews in _gemViewsData.GemViews)
            {
                Object.Destroy(gemViews.gameObject);
            }
            
            _gemViewsData.GemViews.Clear();
        }
    }
}
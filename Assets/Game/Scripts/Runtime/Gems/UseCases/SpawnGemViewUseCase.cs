using Game.Gems.Configurations;
using Game.Gems.Datas;
using Game.Gems.Enums;
using Game.Gems.Views;
using Game.General.Datas;
using GUtils.Optionals;
using UnityEngine;

namespace Game.Gems.UseCases
{
    public sealed class SpawnGemViewUseCase
    {
        readonly GemsConfiguration _gemsConfiguration;
        readonly GeneralSceneData _generalSceneData;
        readonly GemViewsData _gemViewsData;
        readonly GetGemConfigurationByGemTypeUseCase _getGemConfigurationByGemTypeUseCase;

        public SpawnGemViewUseCase(
            GemsConfiguration gemsConfiguration,
            GeneralSceneData generalSceneData,
            GemViewsData gemViewsData,
            GetGemConfigurationByGemTypeUseCase getGemConfigurationByGemTypeUseCase
            )
        {
            _gemsConfiguration = gemsConfiguration;
            _generalSceneData = generalSceneData;
            _gemViewsData = gemViewsData;
            _getGemConfigurationByGemTypeUseCase = getGemConfigurationByGemTypeUseCase;
        }

        public GemView Execute(GemType type, Vector2 worldPosition)
        {
            GemView gemView = Object.Instantiate(_gemsConfiguration.GemViewPrefab, _generalSceneData.Root.transform);
            gemView.transform.position = worldPosition;
            
            _gemViewsData.GemViews.Add(gemView);
            
            Optional<GemConfiguration> optionalGemConfiguration = _getGemConfigurationByGemTypeUseCase.Execute(type);

            bool hasConfiguration = optionalGemConfiguration.TryGet(out GemConfiguration gemConfiguration);

            if (!hasConfiguration)
            {
                Debug.Log($"{nameof(GemConfiguration)}of type {type} could not be found");
            }
            else
            {
                gemView.IconSpriteRenderer.sprite = gemConfiguration.Icon;
            }

            return gemView;
        }
    }
}
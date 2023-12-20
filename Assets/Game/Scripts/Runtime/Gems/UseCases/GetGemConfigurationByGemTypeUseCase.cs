using Game.Gems.Configurations;
using Game.Gems.Enums;
using GUtils.Optionals;

namespace Game.Gems.UseCases
{
    public sealed class GetGemConfigurationByGemTypeUseCase
    {
        readonly GemsConfiguration _gemsConfiguration;

        public GetGemConfigurationByGemTypeUseCase(
            GemsConfiguration gemsConfiguration
            )
        {
            _gemsConfiguration = gemsConfiguration;
        }

        public Optional<GemConfiguration> Execute(GemType gemType)
        {
            foreach (GemConfiguration gemConfiguration in _gemsConfiguration.Gems)
            {
                bool isType = gemConfiguration.Type == gemType;

                if (isType)
                {
                    return gemConfiguration;
                }
            }
            
            return Optional<GemConfiguration>.None;
        }
    }
}
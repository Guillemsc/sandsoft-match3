using Game.Gems.Configurations;
using Game.Grids.Configurations;
using UnityEngine;

namespace Game.General.Configurations
{
    [CreateAssetMenu(fileName = "GeneralConfiguration", menuName = "Game/Configuration/GeneralConfiguration", order = 1)]
    public sealed class GeneralConfiguration : ScriptableObject
    {
        [SerializeField] public GemsConfiguration GemsConfiguration;
        [SerializeField] public GridsConfiguration GridsConfiguration;
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gems.Configurations
{
    [CreateAssetMenu(fileName = "GemConfiguration", menuName = "Game/Configuration/GemConfiguration", order = 1)]
    public sealed class GemsConfiguration : ScriptableObject
    {
        [SerializeField] public List<GemConfiguration> Gems = new();
    }
}
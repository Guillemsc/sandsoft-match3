using System.Collections.Generic;
using Game.Gems.Views;
using UnityEngine;

namespace Game.Gems.Configurations
{
    [CreateAssetMenu(fileName = "GemsConfiguration", menuName = "Game/Configuration/GemsConfiguration", order = 1)]
    public sealed class GemsConfiguration : ScriptableObject
    {
        [SerializeField] public GemView GemViewPrefab;
        [SerializeField] public List<GemConfiguration> Gems = new();
    }
}
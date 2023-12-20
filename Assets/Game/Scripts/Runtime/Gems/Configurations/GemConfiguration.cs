using Game.Gems.Enums;
using UnityEngine;

namespace Game.Gems.Configurations
{
    [CreateAssetMenu(fileName = "GemsConfiguration", menuName = "Game/Configuration/GemsConfiguration", order = 1)]
    public sealed class GemConfiguration : ScriptableObject
    {
        [SerializeField] public GemType Type;
        [SerializeField] public Sprite Icon;
    }
}

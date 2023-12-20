using UnityEngine;

namespace Game.General.Datas
{
    public sealed class GeneralSceneData
    {
        public GameObject Root { get; }
        
        public GeneralSceneData(GameObject root)
        {
            Root = root;
        }
    }
}
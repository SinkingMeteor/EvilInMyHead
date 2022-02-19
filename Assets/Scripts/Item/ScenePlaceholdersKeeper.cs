using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Item
{
    public class ScenePlaceholdersKeeper : MonoBehaviour
    {
        public IReadOnlyList<ItemPlaceholder> ItemPlaceholders => itemPlaceholders;

        [SerializeField][ReadOnly] private ItemPlaceholder[] itemPlaceholders;

#if UNITY_EDITOR
        [Button]
        private void FindAllPlaceholders()
        {
            itemPlaceholders = FindObjectsOfType<ItemPlaceholder>();

        }
#endif
    }
}
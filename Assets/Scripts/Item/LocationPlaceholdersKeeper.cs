using System.Collections.Generic;
using Sheldier.Actors;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Item
{
    public class LocationPlaceholdersKeeper : MonoBehaviour
    {
        public IReadOnlyList<ItemPlaceholder> ItemPlaceholders => itemPlaceholders;
        public IReadOnlyList<ActorPlaceholder> ActorPlaceholders => actorPlaceholders;

        [SerializeField][ReadOnly] private ItemPlaceholder[] itemPlaceholders;
        [SerializeField][ReadOnly] private ActorPlaceholder[] actorPlaceholders;

#if UNITY_EDITOR
        [Button]
        private void FindAllPlaceholders()
        {
            itemPlaceholders = FindObjectsOfType<ItemPlaceholder>();
            actorPlaceholders = FindObjectsOfType<ActorPlaceholder>();

        }
#endif
    }
}
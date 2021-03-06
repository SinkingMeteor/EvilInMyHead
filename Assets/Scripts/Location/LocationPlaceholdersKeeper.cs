using System.Collections.Generic;
using Sheldier.Actors;
using Sheldier.Item;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.GameLocation
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
            itemPlaceholders = GetComponentsInChildren<ItemPlaceholder>();
            actorPlaceholders =  GetComponentsInChildren<ActorPlaceholder>();

        }
#endif
    }
}
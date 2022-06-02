using Sheldier.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Item
{
    public class ReferenceUniqueID : SerializedMonoBehaviour, IUniqueID
    {
        public string ID => persistantUniqueID.Reference;
        [SerializeField] private DataReference persistantUniqueID;
    }
}
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Item
{
    public class UniqueID : SerializedMonoBehaviour, IUniqueID
    {
        public string ID => _id;
        [SerializeField] private string _id;

        public void SetID(string id) => _id = id;
    }
}
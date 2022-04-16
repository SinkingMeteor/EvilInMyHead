using Sheldier.Common;
using Sheldier.Item;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorPlaceholder : SerializedMonoBehaviour
    {
        public string Guid => guid.ID;
        public DataReference Reference => reference;

        [OdinSerialize] private IUniqueID guid;
        [SerializeField] private DataReference reference;
        public void Deactivate() => gameObject.SetActive(false);
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 0.1f);
        }
    }
}
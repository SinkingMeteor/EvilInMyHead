using Sheldier.Common;
using Sheldier.Item;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorPlaceholder : MonoBehaviour
    {
        public DataReference Reference => reference;

        [SerializeField] private DataReference reference;
        public void Deactivate() => gameObject.SetActive(false);
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 0.1f);
        }
    }
}
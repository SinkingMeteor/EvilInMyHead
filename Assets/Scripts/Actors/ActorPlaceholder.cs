using Sheldier.Common.Animation;
using Sheldier.Item;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Actors
{
    [RequireComponent(typeof(UniqueID))]
    public class ActorPlaceholder : MonoBehaviour, IUniqueID
    {
        public string ID => uniqueID.ID;
        public ActorAnimationCollection ActorReference => actorReference;

        [SerializeField] private UniqueID uniqueID;
        [SerializeField] private ActorAnimationCollection actorReference;

        private Material _defaulMaterial;
        public void Deactivate() => gameObject.SetActive(false);
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 0.1f);
        }

        [Button("Setup")]
        private void SetupPlaceholder()
        {
            uniqueID = GetComponent<UniqueID>();
        }
    }
}
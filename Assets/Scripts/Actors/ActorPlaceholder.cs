using Sheldier.Actors.Data;
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
        public ActorAnimationCollection ActorVisualReference => actorVisualReference;
        public ActorData ActorDataReference => actorData;
        public ActorBuildData ActorBuildData => actorBuildData;

        [SerializeField] private ActorAnimationCollection actorVisualReference;
        [SerializeField] private ActorBuildData actorBuildData;
        [SerializeField] private ActorData actorData;
        [SerializeField] private UniqueID uniqueID;

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
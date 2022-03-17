using Sheldier.Actors.Data;
using Sheldier.Item;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Actors
{
    [RequireComponent(typeof(UniqueID))]
    public class ActorPlaceholder : MonoBehaviour, IUniqueID
    {
        public string ID => uniqueID.ID;

        public ActorConfig ActorConfig => actorConfig;
        public ActorBuildData ActorBuildData => actorBuildData;

        [SerializeField] private ActorBuildData actorBuildData;
        [SerializeField] private UniqueID uniqueID;
        [SerializeField] private ActorConfig actorConfig;

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
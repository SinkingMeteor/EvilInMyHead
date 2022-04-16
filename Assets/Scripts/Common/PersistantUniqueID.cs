using Sheldier.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Common
{
    [CreateAssetMenu(menuName = "Sheldier/Common/PersistantUniqueID", fileName = "PersistantUniqueID")]
    public class PersistantUniqueID : BaseScriptableObject
    {
        public string Reference => reference;
    
        [SerializeField,ReadOnly] private string reference;

        [Button]
        private void SetReference(string newReference)
        {
            reference = newReference;
        }
    }
}
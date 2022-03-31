using Sheldier.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Actors.Data
{
    public abstract class IDConfig : BaseScriptableObject
    {
        public string ID => id;
        
        [SerializeField, ReadOnly] private string id;

        [Button]
        private void SetID(string newID) => id = newID;
    }
}
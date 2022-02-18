using Sheldier.ScriptableObjects;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Sheldier.Item
{
    public abstract class IDConfig : BaseScriptableObject
    {
        public string ID => _id;
        [OdinSerialize][ReadOnly] private string _id;

        public void SetID(string id) => _id = id;
    }
}
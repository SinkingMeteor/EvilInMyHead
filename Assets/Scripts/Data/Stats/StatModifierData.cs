using System;

namespace Sheldier.Data
{
    [Serializable]
    public struct StatModifierData
    {
        public string ModifierType;
        public float ModifierValue;
        public float Stack;
    }
}
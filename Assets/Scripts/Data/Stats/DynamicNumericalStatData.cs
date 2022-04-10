using System;
using System.Collections.Generic;

namespace Sheldier.Data
{
    [Serializable]
    public class DynamicNumericalStatData : IDatabaseItem
    {
        public string ID => StatName;
        public float BaseValue => _value;
        public string StatName => _statName;
        public string TypeName => _typeName;
        public IReadOnlyList<StatModifierData> Modifiers => _modifierDatas;

        private string _typeName;
        private string _statName;
        private float _value;
        private List<StatModifierData> _modifierDatas;

        public DynamicNumericalStatData(StaticNumericalStatData staticData)
        {
            _typeName = staticData.TypeName;
            _statName = staticData.StatName;
            _value = staticData.BaseValue;
            _modifierDatas = new List<StatModifierData>();
        }

        public void AddModifier(StatModifierData statModifierData)
        {
            if (_modifierDatas.Contains(statModifierData))
            {
                int index = _modifierDatas.IndexOf(statModifierData);
                StatModifierData data = _modifierDatas[index];
                data.Stack += 1;
                _modifierDatas[index] = data;
                return;
            }
            _modifierDatas.Add(statModifierData);
        }

        public void RemoveModifier(StatModifierData statModifierData)
        {
            if (!_modifierDatas.Contains(statModifierData))
                return;
            int index = _modifierDatas.IndexOf(statModifierData);
            statModifierData.Stack -= 1;
            if(statModifierData.Stack <= 0)
                _modifierDatas.RemoveAt(index);
        }
    }
}
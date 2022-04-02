using System;
using System.Collections.Generic;
using System.Linq;
using Sheldier.Data;
using UnityEngine;

namespace Sheldier.Actors.Data
{
    [Serializable]
    public class ActorDynamicEffectData : IDatabaseItem
    {
        public event Action<int> OnEffectAdded;
        public event Action<int> OnEffectRemoved;
        public string ID => Guid;
        public IReadOnlyList<int> Effects => _effects;

        public string Guid;
        private List<int> _effects;

        public ActorDynamicEffectData(string guid)
        {
            Guid = guid;
            _effects = new List<int>();
        }

        public ActorDynamicEffectData(string guid, int[] effects)
        {
            Guid = guid;
            _effects = effects.ToList();
        }


        public void AddEffect(int id)
        {
            if(_effects.Contains(id))
                return;
            _effects.Add(id);
            OnEffectAdded?.Invoke(id);
        }

        public void RemoveEffect(int id)
        {
            if (!_effects.Contains(id))
                return;
            _effects.Remove(id);
            OnEffectRemoved?.Invoke(id);
        }
    }
}
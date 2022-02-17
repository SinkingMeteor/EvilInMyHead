using Sheldier.Actors.Data;
using UnityEngine;

namespace Sheldier.Gameplay.Effects
{
    public abstract class BaseMovementWrapper : IMovementEffect
    {
        public bool IsExpired => _isExpired;
        public EffectConfig Config => _config;


        protected IMovementEffect _wrappedEntity;
        private bool _isExpired;
        private EffectConfig _config;
        private float _time;
        protected BaseMovementWrapper(EffectConfig config)
        {
            _config = config;
        }

        public void Setup()
        {
            _isExpired = false;
            _time = 5.0f;
        }

        public void Tick()
        {
            _time -= Time.deltaTime;
            if (_time <= 0.0f)
                _isExpired = true;
        }

        public abstract IEffect Clone();

        public void SetWrapper(IMovementEffect effect)
        {
            _wrappedEntity = effect;
        }
        public MovementDataPackage GetMovementData(MovementDataPackage data)
        {
            return GetInternalMovement(data);
        }
        protected abstract MovementDataPackage GetInternalMovement(MovementDataPackage data);
    }
}
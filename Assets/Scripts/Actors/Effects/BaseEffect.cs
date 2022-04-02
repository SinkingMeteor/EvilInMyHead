using Sheldier.Actors;

namespace Sheldier.Gameplay.Effects
{
    public abstract class BaseEffect : IEffect
    {
        public int EffectID => _effectID;
        public virtual bool IsExpired => _timeLeft <= 0;

        protected int _effectID;
        protected float _timeLeft;
        
        private Actor _owner;

        protected BaseEffect(int ID)
        {
            _effectID = ID;
        }

        public virtual void Setup(Actor owner)
        {
            _owner = owner;
        }

        public abstract void Tick();

        public abstract IEffect Clone();
    }
}
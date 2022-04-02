using Sheldier.Actors;

namespace Sheldier.Gameplay.Effects
{
    public interface IEffect
    {
        int EffectID { get; }
        bool IsExpired { get; }
        void Setup(Actor owner);
        void Tick();
        IEffect Clone();
    }
}
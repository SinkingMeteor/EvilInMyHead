using Sheldier.Actors;

namespace Sheldier.Gameplay.Effects
{
    public interface IEffect
    {
        ActorEffectType EffectType { get; }
        bool IsExpired { get; }
        void Setup(Actor owner, float duration);
        void Tick();
        IEffect Clone();
    }
}
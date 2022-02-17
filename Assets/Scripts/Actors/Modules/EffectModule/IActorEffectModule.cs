using Sheldier.Gameplay.Effects;

namespace Sheldier.Actors
{
    public interface IActorEffectModule
    {
        public bool IsEffectExists(IEffect effect);
        public bool IsEffectExists(ActorEffectType effectType);
        public void AddEffect(IEffect effect);
    }
}
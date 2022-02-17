using Sheldier.Gameplay.Effects;

namespace Sheldier.Actors
{
    public class NullEffectModule : IActorEffectModule
    {
        public bool IsEffectExists(IEffect effect) => false;

        public bool IsEffectExists(ActorEffectType effectType) => false;

        public void AddEffect(IEffect effect)
        {
            return;
        }
    }
}
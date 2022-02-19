using System.Collections.Generic;
using Sheldier.Gameplay.Effects;

namespace Sheldier.Actors
{
    public interface IActorEffectModule
    {
        IReadOnlyList<ActorEffectType> EffectCollection { get; }
        bool IsEffectExists(ActorEffectType effectType);
        void AddEffect(ActorEffectType effectType);
    }
}
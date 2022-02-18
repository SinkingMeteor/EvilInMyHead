using System;
using System.Collections.Generic;
using Sheldier.Gameplay.Effects;

namespace Sheldier.Actors
{
    public interface IActorEffectModule
    {
        event Action<IEffect> OnEffectModuleAddedEffect;
        event Action<IEffect> OnEffectModuleRemovedEffect;
        IReadOnlyList<IEffect> EffectCollection { get; }
        bool IsEffectExists(IEffect effect);
        bool IsEffectExists(ActorEffectType effectType);
        void AddEffect(IEffect effect);
    }
}
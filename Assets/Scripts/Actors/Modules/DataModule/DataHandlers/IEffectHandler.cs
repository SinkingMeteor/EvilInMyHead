using System;
using Sheldier.Factories;
using Sheldier.Gameplay.Effects;

namespace Sheldier.Actors.Data
{
    public interface IEffectHandler
    {
        void Initialize(ActorsEffectFactory effectFactory);
        
        event Action<ActorEffectType> OnEffectExpired;
        void AddEffect(ActorEffectType actorEffectType);
        void Tick();
    }
}

using System;

namespace Sheldier.Gameplay.Effects
{
    public interface IEffect
    {
        bool IsExpired { get; }
        EffectConfig Config { get; }

        void Setup();
        void Tick();

        IEffect Clone();
    }
}
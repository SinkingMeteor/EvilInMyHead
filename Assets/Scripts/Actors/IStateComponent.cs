using System;

namespace Sheldier.Actors
{
    public interface IStateComponent
    {
        bool IsLocked { get; }
        bool TransitionConditionIsDone { get; }
        int Priority { get; }
        void Initialize();
        void SetDependencies(ActorInternalData data);
        void Enter();
        void Exit();
        void Tick();
        void FixedTick();
        void Dispose();
    }
}
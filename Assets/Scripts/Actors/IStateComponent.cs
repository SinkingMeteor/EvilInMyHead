using System;

namespace Sheldier.Actors
{
    public interface IStateComponent
    {
        event Action<int> OnNewAnimation;
        bool IsLocked { get; }
        bool TransitionConditionIsDone { get; }
        int Priority { get; }
        void SetDependencies(ActorInputController inputController, ActorTransformHandler actorTransformHandler);
        void Enter();
        void Exit();
        void Tick();
    }
}
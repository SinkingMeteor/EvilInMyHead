using System;

namespace Sheldier.Actors
{
    public interface IStateComponent
    {
        event Action<int> OnNewAnimation;
        bool IsLocked { get; }
        void SetDependencies(ActorStateController actorStateController, ActorInputController inputController, ActorTransformHandler actorTransformHandler);
        void Enter();
        void Exit();
        void Tick();
    }
}
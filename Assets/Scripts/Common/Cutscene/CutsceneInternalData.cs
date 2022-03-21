using Sheldier.Actors;
using Sheldier.Actors.AI;

namespace Sheldier.Common.Cutscene
{
    public class CutsceneInternalData
    {
        public ActorSpawner ActorSpawner => _actorSpawner;
        public Actor CurrentPlayer => _currentPlayer;
        public ActorsAIMoveModule ActorsAIMoveModule => _actorsAIMoveModule;

        public DialoguesProvider DialoguesProvider => _dialoguesProvider;

        private readonly ActorSpawner _actorSpawner;
        private readonly ActorsAIMoveModule _actorsAIMoveModule;
        private readonly Actor _currentPlayer;
        private readonly DialoguesProvider _dialoguesProvider;

        public CutsceneInternalData(ActorSpawner actorSpawner, ActorsAIMoveModule actorsAIMoveModule, Actor currentPlayer, DialoguesProvider dialoguesProvider)
        {
            _currentPlayer = currentPlayer;
            _actorSpawner = actorSpawner;
            _actorsAIMoveModule = actorsAIMoveModule;
            _dialoguesProvider = dialoguesProvider;
        }

    }
}
using System.Collections.Generic;
using Sheldier.Actors.Builder;
using Sheldier.Actors.Data;
using Sheldier.Installers;
using Sheldier.Item;
using Zenject;

namespace Sheldier.Actors
{
    public class ActorSpawner
    {
        public IReadOnlyList<Actor> ActorsOnScene => _actorsOnScene;
        private List<Actor> _actorsOnScene;
        
        private ScenePlaceholdersKeeper _placeholdersKeeper;
        private ActorsInstaller _actorsInstaller;
        private ActorBuilder _actorBuilder;

        public void Initialize(ScenePlaceholdersKeeper placeholdersKeeper)
        {
            _placeholdersKeeper = placeholdersKeeper;
            _actorsOnScene = new List<Actor>();
            LoadItems();
        }

        [Inject]
        private void InjectDependencies(ActorBuilder actorBuilder)
        {
            _actorBuilder = actorBuilder;
        }
        
        private void LoadItems()
        {
            int counter = 0;
            foreach (var placeholder in _placeholdersKeeper.ActorPlaceholders)
            {
                Actor actor = _actorBuilder.Build(placeholder.ActorReference);
                actor.name += counter++;
                actor.transform.position = placeholder.transform.position;
                _actorsOnScene.Add(actor);
                placeholder.Deactivate();
                
            }
        }
        
    }
}
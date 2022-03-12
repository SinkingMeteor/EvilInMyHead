using System.Collections.Generic;
using Sheldier.Actors.Builder;
using Sheldier.Installers;
using Sheldier.Item;

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

        public void SetDependencies(ActorBuilder actorBuilder)
        {
            _actorBuilder = actorBuilder;
        }
        
        private void LoadItems()
        {
            int counter = 0;
            foreach (var placeholder in _placeholdersKeeper.ActorPlaceholders)
            {
                Actor actor = _actorBuilder.Build(placeholder.ActorVisualReference, placeholder.ActorBuildData, placeholder.ActorDataReference);
                actor.name += counter++;
                actor.transform.position = placeholder.transform.position;
                _actorsOnScene.Add(actor);
                placeholder.Deactivate();
                
            }
        }
        
    }
}
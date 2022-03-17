using System.Collections.Generic;
using Sheldier.Actors.Builder;
using Sheldier.Installers;
using Sheldier.Item;

namespace Sheldier.Actors
{
    public class ActorSpawner
    {
        public IReadOnlyDictionary<ActorType, List<Actor>> ActorsOnScene => _actorsOnScene;
        private Dictionary<ActorType, List<Actor>> _actorsOnScene;
        
        private ScenePlaceholdersKeeper _placeholdersKeeper;
        private ActorsInstaller _actorsInstaller;
        private ActorBuilder _actorBuilder;

        public void Initialize(ScenePlaceholdersKeeper placeholdersKeeper)
        {
            _placeholdersKeeper = placeholdersKeeper;
            _actorsOnScene = new Dictionary<ActorType, List<Actor>>();
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
                Actor actor = _actorBuilder.Build(placeholder.ActorConfig, placeholder.ActorBuildData);
                actor.name += counter++;
                actor.transform.position = placeholder.transform.position;
                if(!_actorsOnScene.ContainsKey(placeholder.ActorConfig.ActorType))
                    _actorsOnScene.Add(placeholder.ActorConfig.ActorType, new List<Actor>());
                _actorsOnScene[placeholder.ActorConfig.ActorType].Add(actor);
                placeholder.Deactivate();
                
            }
        }

        public void OnSceneDispose()
        {
            foreach (var actors in _actorsOnScene)
            {
                for (int i = 0; i < actors.Value.Count; i++)
                {
                    actors.Value[i].Dispose();
                }
            }
        }
    }
}
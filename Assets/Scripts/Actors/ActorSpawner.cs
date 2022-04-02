using System.Collections.Generic;
using Sheldier.Actors.Builder;
using Sheldier.Installers;
using Sheldier.Item;

namespace Sheldier.Actors
{
    public class ActorSpawner
    {
        public IReadOnlyDictionary<string, List<Actor>> ActorsOnScene => _actorsOnScene;
        private Dictionary<string, List<Actor>> _actorsOnScene;
        
        private LocationPlaceholdersKeeper _placeholdersKeeper;
        private ActorsInstaller _actorsInstaller;
        private ActorBuilder _actorBuilder;

        public void Initialize(LocationPlaceholdersKeeper placeholdersKeeper)
        {
            _placeholdersKeeper = placeholdersKeeper;
            _actorsOnScene = new Dictionary<string, List<Actor>>();
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
                Actor actor = _actorBuilder.Build(placeholder.Reference.Reference);
                actor.name += counter++;
                actor.transform.position = placeholder.transform.position;
                if(!_actorsOnScene.ContainsKey(placeholder.Reference.Reference))
                    _actorsOnScene.Add(placeholder.Reference.Reference, new List<Actor>());
                _actorsOnScene[placeholder.Reference.Reference].Add(actor);
                placeholder.Deactivate();
                
            }
        }

        public void OnLocationDispose()
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
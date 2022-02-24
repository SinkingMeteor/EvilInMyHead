using System.Collections.Generic;
using Sheldier.Installers;
using Sheldier.Item;
using UnityEngine;
using Zenject;

namespace Sheldier.Actors
{
    public class ActorSpawner
    {
        public IReadOnlyList<Actor> ActorsOnScene => _actorsOnScene;
        private List<Actor> _actorsOnScene;
        
        private ScenePlaceholdersKeeper _placeholdersKeeper;
        private ActorsMap _actorsMap;
        private ActorsInstaller _actorsInstaller;

        public void Initialize(ScenePlaceholdersKeeper placeholdersKeeper)
        {
            _placeholdersKeeper = placeholdersKeeper;
            _actorsOnScene = new List<Actor>();
            LoadItems();
        }

        [Inject]
        private void InjectDependencies(ActorsMap actorsMap, ActorsInstaller actorsInstaller)
        {
            _actorsInstaller = actorsInstaller;
            _actorsMap = actorsMap;
        }
        
        private void LoadItems()
        {
            int counter = 0;
            foreach (var placeholder in _placeholdersKeeper.ActorPlaceholders)
            {
                Actor actor = GameObject.Instantiate(_actorsMap.Actors[placeholder.ActorReference]);
                actor.name += counter++;
                actor.transform.position = placeholder.transform.position;
                _actorsInstaller.InjectActor(actor);
                actor.Initialize();
                _actorsOnScene.Add(actor);
                placeholder.Deactivate();
                
            }
        }
        
    }
}
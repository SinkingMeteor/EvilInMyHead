using System.Collections.Generic;
using System.Linq;
using Sheldier.Data;
using UnityEngine;

namespace Sheldier.Actors.Data
{
    public class SceneActorsDatabase
    {
        private Dictionary<string, List<Actor>> _sceneActors;
        private readonly Database<ActorDynamicConfigData> _dynamicActorConfigDatabase;

        public SceneActorsDatabase(Database<ActorDynamicConfigData> dynamicActorConfigDatabase)
        {
            _dynamicActorConfigDatabase = dynamicActorConfigDatabase;
            _sceneActors = new Dictionary<string, List<Actor>>();
        }

        public bool ContainsKey(string typeID) => _sceneActors.ContainsKey(typeID);

        public Actor GetFirst(string typeName)
        {
            return _sceneActors[typeName][0];
        }

        public Actor GetFirst()
        {
            return _sceneActors.First().Value[0];
        }
        public Actor Get(string guid)
        {
            var typeName = _dynamicActorConfigDatabase.Get(guid).TypeName;
            return _sceneActors[typeName].Find(x => x.Guid == guid);
        }
        public void Add(string typeName, Actor actor)
        {
            if(!_sceneActors.ContainsKey(typeName))
                _sceneActors.Add(typeName, new List<Actor>());
            _sceneActors[typeName].Add(actor);
        }

        public void Remove(string typeName, Actor actor)
        {
            if (_sceneActors.ContainsKey(typeName))
                _sceneActors[typeName].Remove(actor);
        }

        public void Clear()
        {
            foreach (var actorList in _sceneActors)
            {
                foreach (var actor in actorList.Value)
                {
                    actor.Dispose();
                    GameObject.Destroy(actor.gameObject);
                }
            }
            _sceneActors.Clear();
        }


    }
}
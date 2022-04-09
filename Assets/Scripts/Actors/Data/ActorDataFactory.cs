using System;
using Sheldier.Actors.Data;
using Sheldier.Data;
using Zenject;

namespace Sheldier.Actors.Data
{
    public class ActorDataFactory
    {
        private Database<ActorDynamicConfigData> _dynamicConfigDatabase;
        private Database<ActorDynamicMovementData> _dynamicMovementDatabase;
        private Database<ActorDynamicEffectData> _dynamicEffectDatabase;

        private Database<ActorStaticConfigData> _staticConfigDatabase;
        private Database<ActorStaticMovementData> _staticMovementDatabase;
        private Database<ActorStaticBuildData> _staticBuildDatabase;

        [Inject]
        public void InjectDependencies(Database<ActorDynamicConfigData> dynamicConfigDatabase, Database<ActorDynamicMovementData> dynamicMovementDatabase,
             Database<ActorDynamicEffectData> dynamicEffectDatabase, Database<ActorStaticConfigData> staticConfigDatabase,
            Database<ActorStaticMovementData> staticMovementDatabase, Database<ActorStaticDialogueData> staticDialogueDatabase, Database<ActorStaticBuildData> staticBuildDatabase)
        {
            
            _staticBuildDatabase = staticBuildDatabase;
            _staticConfigDatabase = staticConfigDatabase;
            _staticMovementDatabase = staticMovementDatabase;
            
            _dynamicMovementDatabase = dynamicMovementDatabase;
            _dynamicConfigDatabase = dynamicConfigDatabase;
            _dynamicEffectDatabase = dynamicEffectDatabase;
        }

        public ActorStaticConfigData GetStaticConfigData(string typeID)
        {
            return _staticConfigDatabase.Get(typeID);
        }

        public ActorDynamicConfigData GetDynamicConfigData(string guid)
        {
            return _dynamicConfigDatabase.Get(guid);
        }
        public ActorDynamicConfigData GetDynamicActorConfig(string typeID, string guid)
        {
            if (!_staticConfigDatabase.TryGet(typeID, out ActorStaticConfigData staticConfig))
                throw new NullReferenceException($"Can't create dynamic config with {typeID} reference");

            if (_dynamicConfigDatabase.IsItemExists(guid))
                return _dynamicConfigDatabase.Get(guid);
            
            string newGuid = GenerateNewGuid();
            CreateStats(newGuid);
            ActorDynamicConfigData dynamicConfigData = new ActorDynamicConfigData(newGuid, staticConfig);
            _dynamicConfigDatabase.Add(dynamicConfigData);
            return dynamicConfigData;
        }

  

        public bool IsDynamicConfigExists(string guid)
        {
            return _dynamicConfigDatabase.IsItemExists(guid);
        }
        
        private void CreateStats(string newGuid)
        {
        }
        
        public void RemoveDynamicActorConfig(string guid)
        {
            if (!_staticConfigDatabase.IsItemExists(guid))
                return;
            _dynamicConfigDatabase.Remove(guid);
        }

        public ActorDynamicMovementData GetDynamicMovementData(string guid)
        {
            if (!_dynamicConfigDatabase.TryGet(guid, out ActorDynamicConfigData dynamicConfigData))
                throw new NullReferenceException(
                    $"You are trying to create movement data before creating config. Config is a heart of actors data.");
            if(!_staticMovementDatabase.TryGet(dynamicConfigData.TypeName, out ActorStaticMovementData staticMovementData))
                throw new NullReferenceException($"Can't create dynamic config with {dynamicConfigData.TypeName} reference due to absence of static movement data");
            if (_dynamicMovementDatabase.IsItemExists(guid))
                return _dynamicMovementDatabase.Get(guid);
                
            ActorDynamicMovementData dynamicMovementData = new ActorDynamicMovementData(dynamicConfigData.ID, staticMovementData);
            _dynamicMovementDatabase.Add(dynamicMovementData);
            return dynamicMovementData;
        }

        public void RemoveDynamicMovementData(string guid)
        {
            if (!_staticMovementDatabase.IsItemExists(guid))
                return;
            _dynamicMovementDatabase.Remove(guid);
        }

        public ActorDynamicEffectData CreateDynamicEffectData(string typeID)
        {
            if (!_dynamicConfigDatabase.TryGet(typeID, out ActorDynamicConfigData dynamicConfigData))
                throw new NullReferenceException(
                    $"You are trying to create effect data before creating config. Config is a heart of actors data.");
            if (_dynamicEffectDatabase.IsItemExists(dynamicConfigData.Guid))
                return _dynamicEffectDatabase.Get(dynamicConfigData.Guid);
            
            ActorDynamicEffectData dynamicEffectData = new ActorDynamicEffectData(dynamicConfigData.Guid);
            _dynamicEffectDatabase.Add(dynamicEffectData);
            return dynamicEffectData;
        }
        
        public ActorStaticBuildData GetBuildData(string typeID)
        {
            if(!_staticBuildDatabase.TryGet(typeID, out ActorStaticBuildData buildData))
                throw new NullReferenceException($"Can't find build data for actor {typeID}");
            return buildData;
        }
        private string GenerateNewGuid() => Guid.NewGuid().ToString();

        
    }
}
using System;
using Sheldier.Actors.Data;
using Sheldier.Data;
using Zenject;

namespace Sheldier.Actors.Builder
{
    public class ActorDataFactory
    {
        private Database<ActorDynamicConfigData> _dynamicConfigDatabase;
        private Database<ActorDynamicMovementData> _dynamicMovementDatabase;
        private Database<ActorDynamicDialogueData> _dynamicDialogueDatabase;
        private Database<ActorDynamicEffectData> _dynamicEffectDatabase;

        private Database<ActorStaticConfigData> _staticConfigDatabase;
        private Database<ActorStaticMovementData> _staticMovementDatabase;
        private Database<ActorStaticDialogueData> _staticDialogueDatabase;
        private Database<ActorStaticBuildData> _staticBuildDatabase;

        [Inject]
        public void InjectDependencies(Database<ActorDynamicConfigData> dynamicConfigDatabase, Database<ActorDynamicMovementData> dynamicMovementDatabase,
            Database<ActorDynamicDialogueData> dynamicDialogueDatabase, Database<ActorDynamicEffectData> dynamicEffectDatabase, Database<ActorStaticConfigData> staticConfigDatabase,
            Database<ActorStaticMovementData> staticMovementDatabase, Database<ActorStaticDialogueData> staticDialogueDatabase, Database<ActorStaticBuildData> staticBuildDatabase)
        {
            
            _staticBuildDatabase = staticBuildDatabase;
            _staticConfigDatabase = staticConfigDatabase;
            _staticMovementDatabase = staticMovementDatabase;
            _staticDialogueDatabase = staticDialogueDatabase;
            
            _dynamicMovementDatabase = dynamicMovementDatabase;
            _dynamicConfigDatabase = dynamicConfigDatabase;
            _dynamicDialogueDatabase = dynamicDialogueDatabase;
            _dynamicEffectDatabase = dynamicEffectDatabase;
        }

        public ActorDynamicConfigData CreateDynamicActorConfig(string typeID)
        {
            if (!_staticConfigDatabase.TryGet(typeID, out ActorStaticConfigData staticConfig))
                throw new NullReferenceException($"Can't create dynamic config with {typeID} reference");
            string newGuid = GenerateNewGuid();
            ActorDynamicConfigData dynamicConfigData = new ActorDynamicConfigData(newGuid, staticConfig);
            _dynamicConfigDatabase.Add(newGuid, dynamicConfigData);
            return dynamicConfigData;
        }

        public void RemoveDynamicActorConfig(string guid)
        {
            if (!_staticConfigDatabase.IsItemExists(guid))
                return;
            _dynamicConfigDatabase.Remove(guid);
        }

        public ActorDynamicMovementData CreateDynamicMovementData(string guid)
        {
            if (!_dynamicConfigDatabase.TryGet(guid, out ActorDynamicConfigData dynamicConfigData))
                throw new NullReferenceException(
                    $"You are trying to create movement data before creating config. Config is a heart of actors data.");
            if(!_staticMovementDatabase.TryGet(dynamicConfigData.NameID, out ActorStaticMovementData staticMovementData))
                throw new NullReferenceException($"Can't create dynamic config with {dynamicConfigData.NameID} reference due to absence of static movement data");
            
            ActorDynamicMovementData dynamicMovementData = new ActorDynamicMovementData(dynamicConfigData.ID, staticMovementData);
            _dynamicMovementDatabase.Add(dynamicMovementData.ID, dynamicMovementData);
            return dynamicMovementData;
        }

        public void RemoveDynamicMovementData(string guid)
        {
            if (!_staticMovementDatabase.IsItemExists(guid))
                return;
            _dynamicMovementDatabase.Remove(guid);
        }

        public ActorDynamicDialogueData CreateDynamicDialogueData(string guid)
        {
            if (!_dynamicConfigDatabase.TryGet(guid, out ActorDynamicConfigData dynamicConfigData))
                throw new NullReferenceException(
                    $"You are trying to create dialogue data before creating config. Config is a heart of actors data.");
            if(!_staticDialogueDatabase.TryGet(dynamicConfigData.NameID, out ActorStaticDialogueData staticDialogueData))
                throw new NullReferenceException($"Can't create dynamic config with {dynamicConfigData.NameID} reference due to absence of static dialogue data");
            
            ActorDynamicDialogueData dynamicDialogueData = new ActorDynamicDialogueData(dynamicConfigData.ID, staticDialogueData);
            _dynamicDialogueDatabase.Add(dynamicDialogueData.ID, dynamicDialogueData);
            return dynamicDialogueData;
        }

        public void RemoveDynamicDialogueData(string guid)
        {
            if (!_staticDialogueDatabase.IsItemExists(guid))
                return;
            _dynamicDialogueDatabase.Remove(guid);
        }

        public ActorDynamicEffectData CreateDynamicEffectData(string typeID)
        {
            if (!_dynamicConfigDatabase.TryGet(typeID, out ActorDynamicConfigData dynamicConfigData))
                throw new NullReferenceException(
                    $"You are trying to create effect data before creating config. Config is a heart of actors data.");
            ActorDynamicEffectData dynamicEffectData = new ActorDynamicEffectData(dynamicConfigData.ID);
            _dynamicEffectDatabase.Add(dynamicEffectData.ID, dynamicEffectData);
            return dynamicEffectData;
        }

        public void RemoveDynamicEffectData(string guid)
        {
            
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
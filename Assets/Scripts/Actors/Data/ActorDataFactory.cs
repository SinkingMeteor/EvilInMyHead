using System;
using System.Collections.Generic;
using Sheldier.Data;
using Zenject;

namespace Sheldier.Actors.Data
{
    public class ActorDataFactory
    {
        private Database<ActorDynamicConfigData> _dynamicConfigDatabase;
        private Database<ActorDynamicEffectData> _dynamicEffectDatabase;

        private Database<ActorStaticConfigData> _staticConfigDatabase;
        private Database<ActorStaticBuildData> _staticBuildDatabase;
        
        private Database<StaticNumericalStatCollection> _staticNumericalStatDatabase;
        private Database<DynamicNumericalEntityStatsCollection> _dynamicGeneralNumericalStatsDatabase;
        
        private Database<StaticStringStatCollection> _staticStringStatDatabase;
        private Database<DynamicStringEntityStatsCollection> _dynamicGeneralStringStatsDatabase;

        [Inject]
        public void InjectDependencies(Database<ActorDynamicConfigData> dynamicConfigDatabase,
                                       Database<ActorDynamicEffectData> dynamicEffectDatabase,
                                       Database<StaticNumericalStatCollection> staticNumericalStatDatabase,
                                       Database<StaticStringStatCollection> staticStringStatDatabase,
                                       Database<DynamicNumericalEntityStatsCollection> dynamicGeneralNumericalStatsDatabase,
                                       Database<DynamicStringEntityStatsCollection> dynamicGeneralStringStatsDatabase,
                                       Database<ActorStaticConfigData> staticConfigDatabase,
                                       Database<ActorStaticBuildData> staticBuildDatabase)
        {
            
            _staticBuildDatabase = staticBuildDatabase;
            _staticConfigDatabase = staticConfigDatabase;
            _dynamicConfigDatabase = dynamicConfigDatabase;
            _dynamicEffectDatabase = dynamicEffectDatabase;

            _staticNumericalStatDatabase = staticNumericalStatDatabase;
            _staticStringStatDatabase = staticStringStatDatabase;
            _dynamicGeneralNumericalStatsDatabase = dynamicGeneralNumericalStatsDatabase;
            _dynamicGeneralStringStatsDatabase = dynamicGeneralStringStatsDatabase;
        }

        public ActorStaticConfigData GetStaticConfigData(string typeID)
        {
            return _staticConfigDatabase.Get(typeID);
        }

        public ActorDynamicConfigData GetDynamicConfigData(string guid)
        {
            return _dynamicConfigDatabase.Get(guid);
        }

        public DynamicNumericalEntityStatsCollection GetEntityNumericalStatCollection(string guid)
        {
            return _dynamicGeneralNumericalStatsDatabase.Get(guid);
        }
        public DynamicStringEntityStatsCollection GetEntityStringStatCollection(string guid)
        {
            return _dynamicGeneralStringStatsDatabase.Get(guid);
        }
        public void RemoveDynamicActorConfig(string guid)
        {
            if (!_staticConfigDatabase.IsItemExists(guid))
                return;
            _dynamicConfigDatabase.Remove(guid);
        }
        
        public ActorDynamicConfigData GetDynamicActorConfig(string typeID, string guid)
        {
            if (!_staticConfigDatabase.TryGet(typeID, out ActorStaticConfigData staticConfig))
                throw new NullReferenceException($"Can't create dynamic config with {typeID} reference");

            if (_dynamicConfigDatabase.IsItemExists(guid))
                return _dynamicConfigDatabase.Get(guid);
            
            ActorDynamicConfigData dynamicConfigData = new ActorDynamicConfigData(guid, staticConfig);
            _dynamicConfigDatabase.Add(dynamicConfigData);
            CreateStats(dynamicConfigData);
            return dynamicConfigData;
        }

        public bool IsDynamicConfigExists(string guid)
        {
            return _dynamicConfigDatabase.IsItemExists(guid);
        }
        
        private void CreateStats(ActorDynamicConfigData dynamicConfigData)
        {
            string guid = dynamicConfigData.Guid;
            string typeName = dynamicConfigData.TypeName;

            CreateNumericalStats(typeName, guid);
            CreateStringStats(typeName, guid);
        }

        private void CreateNumericalStats(string typeName, string guid)
        {
            StaticNumericalStatCollection staticNumericalStatsCollection = _staticNumericalStatDatabase.Get(typeName);
            List<DynamicNumericalStatData> dynamicNumericalStats = new List<DynamicNumericalStatData>();
            foreach (var staticStat in staticNumericalStatsCollection.StatsCollection.Values)
            {
                dynamicNumericalStats.Add(new DynamicNumericalStatData(staticStat));
            }

            DynamicNumericalEntityStatsCollection entityCollection = new DynamicNumericalEntityStatsCollection(guid);
            entityCollection.Set(dynamicNumericalStats);
            _dynamicGeneralNumericalStatsDatabase.Add(entityCollection);
        }
        private void CreateStringStats(string typeName, string guid)
        {
            StaticStringStatCollection staticStringStatsCollection = _staticStringStatDatabase.Get(typeName);
            List<DynamicStringStatData> dynamicStringStats = new List<DynamicStringStatData>();
            foreach (var staticStat in staticStringStatsCollection.StatsCollection.Values)
            {
                dynamicStringStats.Add(new DynamicStringStatData(staticStat));
            }

            DynamicStringEntityStatsCollection entityCollection = new DynamicStringEntityStatsCollection(guid);
            entityCollection.Set(dynamicStringStats);
            _dynamicGeneralStringStatsDatabase.Add(entityCollection);
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
        
    }
}
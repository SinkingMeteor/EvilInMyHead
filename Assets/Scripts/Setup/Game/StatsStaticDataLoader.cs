using Sheldier.Common;
using Sheldier.Constants;
using Sheldier.Data;
using UnityEngine;
using Zenject;

namespace Sheldier.Setup
{
    public class StatsStaticDataLoader
    {
        private Database<StaticNumericalStatData> _staticNumericalStatDatabase;
        private Database<StaticStringStatData> _staticStringStatDatabase;
        private AssetProvider<TextAsset> _dataLoader;

        [Inject]
        private void InjectDependencies(Database<StaticNumericalStatData> staticNumericalStatDatabase, Database<StaticStringStatData> staticStringStatDatabase,
            AssetProvider<TextAsset> dataLoader)
        {
            _dataLoader = dataLoader;
            _staticStringStatDatabase = staticStringStatDatabase;
            _staticNumericalStatDatabase = staticNumericalStatDatabase;
        }

        public void LoadStaticData()
        {
            _staticNumericalStatDatabase.FillDatabase(_dataLoader.Get(AssetPathConstants.STAT_NUMERICAL_CONFIG));
            _staticStringStatDatabase.FillDatabase(_dataLoader.Get(AssetPathConstants.STAT_STRING_CONFIG));
        }
    }
}
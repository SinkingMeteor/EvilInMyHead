using Sheldier.Common;
using Sheldier.Constants;
using Sheldier.Data;
using Sheldier.UI;
using UnityEditor.VersionControl;
using UnityEngine;
using Zenject;

namespace Sheldier.Setup
{
    public class UIStaticDataLoader
    {
        private Database<UIPerformStaticData> _staticUIPerformDatabase;
        private AssetProvider<TextAsset> _dataLoader;

        public UIStaticDataLoader(Database<UIPerformStaticData> staticUIPerformDatabase, AssetProvider<TextAsset> dataLoader)
        {
            _dataLoader = dataLoader;
            _staticUIPerformDatabase = staticUIPerformDatabase;
        }

        public void LoadStaticData()
        {
            _staticUIPerformDatabase.FillDatabase(_dataLoader.Get(AssetPathConstants.UI_PERFORM_CONFIG));
        }
    }
}
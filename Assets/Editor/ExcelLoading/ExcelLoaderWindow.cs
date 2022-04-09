using Sheldier.Data;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace SheldierEditor
{
    public class ExcelLoaderWindow : OdinEditorWindow
    {
        private GameObject _excelLoaderParent;

        [MenuItem("Sheldier/ExcelLoader")]
        public static void Open()
        {
            GetWindow<ExcelLoaderWindow>().Show();
        }

        private void OnBecameVisible()
        {
            LoadExcelLoader();
        }
        private void LoadExcelLoader()
        {
            _excelLoaderParent = Resources.Load<GameObject>("ExcelLoader");
        }

        [FoldoutGroup("Actors")]
        [Button]
        private void LoadActorConfigs()
        {
            _excelLoaderParent.GetComponentInChildren<ActorsConfigImporter>().ActorStaticConfigToJson();
        }

        [FoldoutGroup("Actors")]
        [Button]
        private void LoadActorBuildDatas()
        {
            _excelLoaderParent.GetComponentInChildren<ActorsConfigImporter>().ActorStaticBuildDataToJson();
        }
        
        [FoldoutGroup("Items")]
        [Button]
        private void LoadItemsConfigs()
        {
            _excelLoaderParent.GetComponentInChildren<ItemsConfigImporter>().ItemStaticConfigToJson();
        }

        [FoldoutGroup("Items")]
        [Button]
        private void LoadItemsWeaponData()
        {
            _excelLoaderParent.GetComponentInChildren<ItemsConfigImporter>().ItemStaticWeaponDataToJson();
        }
        
        [FoldoutGroup("Items")]
        [Button]
        private void LoadItemsProjectileData()
        {
            _excelLoaderParent.GetComponentInChildren<ItemsConfigImporter>().ItemStaticProjectileDataToJson();
        }
        [FoldoutGroup("Items")]
        [Button]
        private void LoadItemsInventorySlotData()
        {
            _excelLoaderParent.GetComponentInChildren<ItemsConfigImporter>().ItemStaticInventorySlotDataToJson();
        }

        [FoldoutGroup("Stats")]
        [Button]
        private void LoadNumericalData()
        {
            _excelLoaderParent.GetComponentInChildren<StatsConfigImporter>().NumericalStatsDataToJson();
        }
        
        [FoldoutGroup("Stats")]
        [Button]
        private void LoadStringData()
        {
            _excelLoaderParent.GetComponentInChildren<StatsConfigImporter>().StringStatsDataToJson();
        }
    }

}

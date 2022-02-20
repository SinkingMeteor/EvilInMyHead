using Sheldier.Item;
using UnityEditor;
using UnityEngine;

namespace SheldierEditor.Item
{
    [CustomEditor(typeof(GunWeaponConfig))]
    public class WeaponConfigEditor : Editor
    {
        private WeaponPreview _weaponPreview = new WeaponPreview();
        
        void OnEnable()
        {
            _weaponPreview.Init((GunWeaponConfig)target);
        }

        void OnDisable()
        {
            _weaponPreview.Destroy();
        }
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Rect previewRect = EditorGUILayout.GetControlRect(false, WeaponPreview.IconSize);

            var texture = _weaponPreview.Render();
            GUI.DrawTexture(previewRect, texture, ScaleMode.ScaleToFit, false);
        }
    }
}

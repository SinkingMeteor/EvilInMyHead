using Sheldier.Common.Animation;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace SheldierEditor.Item
{
    [CustomEditor(typeof(AnimationData))][ExecuteInEditMode]
    public class AnimationDataEditor : Editor
    {
        private AnimationDataPreview _weaponPreview = new AnimationDataPreview();
        
        void OnEnable()
        {
            _weaponPreview.Init((AnimationData)target);
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
            EditorUtility.SetDirty(target);
        }
    }
}
using Sheldier.Item;
using UnityEditor;
using UnityEngine;

namespace SheldierEditor.Item
{
    public class WeaponPreview
    {
        public const int IconSize = 128;

        private WeaponConfig _weaponConfig;
        private GameObject _previewGameObject;
        private GameObject _pointerGameObject;
        private PreviewRenderUtility _previewRenderUtility;
        private Texture2D _backgroundTexture;
        private GUIStyle _guiStyle = new GUIStyle();

        public void Init(WeaponConfig weapon)
        {
            Destroy();

            _weaponConfig = weapon;

            _previewGameObject = new GameObject("Weapon");
            var spriteRenderer = _previewGameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = weapon.Icon;
            _previewGameObject.hideFlags = HideFlags.HideAndDontSave;
            _previewGameObject.transform.localScale = Vector3.one;


            _pointerGameObject = new GameObject("Pointer");
            var pointerSpriteRenderer = _pointerGameObject.AddComponent<SpriteRenderer>();
            pointerSpriteRenderer.sprite = Resources.Load<Sprite>("AimPointer");
            pointerSpriteRenderer.sortingOrder = 1;
            _pointerGameObject.hideFlags = HideFlags.HideAndDontSave;
            _pointerGameObject.transform.localScale = Vector3.one;

            _previewRenderUtility = new PreviewRenderUtility();
            _previewRenderUtility.camera.orthographic = true;
            _previewRenderUtility.camera.transform.localScale = Vector3.one;
            _previewRenderUtility.camera.nearClipPlane = 0.1f;
            _previewRenderUtility.camera.farClipPlane = 20.0f;
            _previewRenderUtility.AddSingleGO(_pointerGameObject);
            _previewRenderUtility.AddSingleGO(_previewGameObject);
        }

        public void Destroy()
        {
            if (_previewRenderUtility != null) {
                _previewRenderUtility.Cleanup();
                _previewRenderUtility = null;
            }

            if (_previewGameObject != null) {
                GameObject.DestroyImmediate(_previewGameObject);
                _previewGameObject = null;
            }

            if (_backgroundTexture != null) {
                GameObject.DestroyImmediate(_backgroundTexture);
                _backgroundTexture = null;
            }
        }

        public Texture Render()
        {
            _previewRenderUtility.camera.orthographicSize = _weaponConfig.CameraSize;

            var cameraTransform = _previewRenderUtility.camera.transform;
            cameraTransform.position = new Vector3(_weaponConfig.CameraXPosition, _weaponConfig.CameraYPosition, 0.0f);
            cameraTransform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

            var objectTransform = _previewGameObject.transform;
            objectTransform.position = new Vector3(0.0f, 0.0f, 0.5f);
            objectTransform.localScale = Vector3.one;
            objectTransform.rotation = Quaternion.identity;

            var pointerTransform = _pointerGameObject.transform;
            pointerTransform.position = new Vector3(_weaponConfig.AimLocalPosition.x, _weaponConfig.AimLocalPosition.y, 0.5f);            
            pointerTransform.localScale = Vector3.one;
            pointerTransform.rotation = Quaternion.identity;

            _previewRenderUtility.BeginPreview(new Rect(0, 0, IconSize, IconSize), _guiStyle);
            _previewRenderUtility.Render(true);
            return _previewRenderUtility.EndPreview();
        }
    }
}
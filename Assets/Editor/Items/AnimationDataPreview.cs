using Sheldier.Common.Animation;
using UnityEditor;
using UnityEngine;

namespace SheldierEditor.Item
{
    public class AnimationDataPreview
    {
        public const int IconSize = 128;

        private AnimationData _animationData;
        private GameObject _previewGameObject;
        private GameObject _pointerGameObject;
        private PreviewRenderUtility _previewRenderUtility;
        private Texture2D _backgroundTexture;
        private GUIStyle _guiStyle = new GUIStyle();
        private float _currentFrame;
        private SpriteRenderer _spriteRenderer;

        public void Init(AnimationData animationData)
        {
            Destroy();

            _animationData = animationData;

            _previewGameObject = new GameObject();
            _spriteRenderer = _previewGameObject.AddComponent<SpriteRenderer>();
            _previewGameObject.hideFlags = HideFlags.HideAndDontSave;
            _previewGameObject.transform.localScale = Vector3.one;

            _previewRenderUtility = new PreviewRenderUtility();
            _previewRenderUtility.camera.orthographic = true;
            _previewRenderUtility.camera.transform.localScale = Vector3.one;
            _previewRenderUtility.camera.nearClipPlane = 0.1f;
            _previewRenderUtility.camera.farClipPlane = 20.0f;
            _previewRenderUtility.AddSingleGO(_previewGameObject);

            _currentFrame = 0.0f;
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
            _previewRenderUtility.camera.orthographicSize = 0.226f;

            var cameraTransform = _previewRenderUtility.camera.transform;
            cameraTransform.position = new Vector3(0.069f, 0.03f, 0.0f);
            cameraTransform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

            var objectTransform = _previewGameObject.transform;
            objectTransform.position = new Vector3(0.0f, 0.0f, 0.5f);
            objectTransform.localScale = Vector3.one;
            objectTransform.rotation = Quaternion.identity;
            
            int frameCount = _animationData.Frames.Length;
            _currentFrame = (_currentFrame + Time.deltaTime * 6) % frameCount;
            int frameIndex = (int) _currentFrame;
            _spriteRenderer.sprite = _animationData.Frames[frameIndex];

            _previewRenderUtility.BeginPreview(new Rect(0, 0, IconSize, IconSize), _guiStyle);
            _previewRenderUtility.Render(true);
            return _previewRenderUtility.EndPreview();
        }
    }
}
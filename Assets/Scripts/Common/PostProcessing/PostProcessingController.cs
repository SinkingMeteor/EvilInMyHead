using Sheldier.Data;
using UnityEngine;
using UnityEngine.Rendering;

namespace Sheldier.Common
{
    public class PostProcessingController
    {
        private readonly AssetProvider<VolumeProfile> _postProcessingLoader;
        private Volume _sceneVolume;

        public PostProcessingController(AssetProvider<VolumeProfile> postProcessingLoader)
        {
            _postProcessingLoader = postProcessingLoader;
        }

        public void InitializeOnScene()
        {
            GameObject volumeGameObject = new GameObject("[POST_PROCESSING]");
            _sceneVolume = volumeGameObject.AddComponent<Volume>();
        }

        public void ApplyPostProcessingProfile(string typeName)
        {
            VolumeProfile profile = _postProcessingLoader.Get(typeName);
            _sceneVolume.profile = profile;
        }

        public void Dispose()
        {
            _postProcessingLoader.Clear();
        }
    }
}
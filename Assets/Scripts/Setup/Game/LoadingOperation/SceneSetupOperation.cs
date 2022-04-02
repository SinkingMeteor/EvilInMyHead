using System;
using System.Threading.Tasks;
using Sheldier.Common;
using Sheldier.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sheldier.Setup
{
    public class SceneSetupOperation : ILoadOperation
    {
        private SceneData _sceneData;
        public string LoadLabel => "Setup Scene";
        
        public SceneSetupOperation()
        {
            _sceneData = ResourceLoader.Load<SceneData>(ResourcePaths.COLONY_SCENE_DATA_PATH);
        }
        public void SetTargetScene(SceneData targetSceneData)
        {
            _sceneData = targetSceneData;
        }
        public async Task Load(Action<float> SetProgress)
        {
            Scene scene = SceneManager.GetSceneByName(_sceneData.SceneName);
            ISceneStartup startUp = null;
            foreach (var rootGameObject in scene.GetRootGameObjects())
            {
                if(rootGameObject.TryGetComponent<ISceneStartup>(out startUp))
                    break;
            }
            if(startUp == null)
                throw new InvalidOperationException($"Can't find startup into a scene {_sceneData.SceneName}");

            await startUp.StartScene();
            SetProgress(1.0f);
        }
    }
}
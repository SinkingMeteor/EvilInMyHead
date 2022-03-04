using System;
using System.Threading.Tasks;
using Sheldier.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sheldier.Setup
{
    public class SceneLoadingOperation : ILoadOperation
    {
        private SceneData _sceneData;
        public string LoadLabel => "Loading Scene";
        
        public SceneLoadingOperation()
        {
            _sceneData = Resources.Load<SceneData>(ResourcePaths.COLONY_SCENE_DATA_PATH);
        }
        public void SetTargetScene(SceneData targetSceneData)
        {
            _sceneData = targetSceneData;
        }
        public async Task Load(Action<float> SetProgress)
        {
            var loadedScene = SceneManager.LoadSceneAsync(_sceneData.SceneName, LoadSceneMode.Single);
            while (!loadedScene.isDone)
            {
                await Task.Delay(1);
            }
            SetProgress(1.0f);
            
        }
    }
}
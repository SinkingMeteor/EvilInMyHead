using System;
using System.Threading.Tasks;
using Sheldier.Constants;
using UnityEngine.SceneManagement;

namespace Sheldier.Setup
{
    public class SceneLoadingOperation : ILoadOperation
    {
        private string[] _targetSceneNames;
        public string LoadLabel => "Loading Scene";
        public SceneLoadingOperation()
        {
            //Default
            _targetSceneNames = new[] { SceneNames.COLONY_OUTSIDE};
        }

        public void SetTargetScene(string[] targetSceneNames)
        {
            _targetSceneNames = targetSceneNames;
        }

        public void SetTargetScene(string targetSceneName)
        {
            _targetSceneNames = new[] {targetSceneName};
        }


        public async Task Load(Action<float> SetProgress)
        {
            float divider = 1.0f / _targetSceneNames.Length;

            for (int i = 0; i < _targetSceneNames.Length; i++)
            {
                var loadedScene = SceneManager.LoadSceneAsync(_targetSceneNames[i], i == 0 ? LoadSceneMode.Single : LoadSceneMode.Additive);
                SetProgress(divider * i);
                while (!loadedScene.isDone)
                {
                    await Task.Delay(1);
                }
            }
            SetProgress(1.0f);
        }
    }
}
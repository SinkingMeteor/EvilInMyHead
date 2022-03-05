using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sheldier.Constants;
using Sheldier.UI;
using UnityEngine;
using Zenject;

namespace Sheldier.Setup
{
    public class UILoadingOperation : ILoadOperation
    {
        private UIStatesController _statesController;
        private SceneData _sceneData;
        public string LoadLabel => "Loading UI";

        public UILoadingOperation()
        {
            _sceneData = Resources.Load<SceneData>(ResourcePaths.COLONY_SCENE_DATA_PATH);
        }
        
        [Inject]
        public void InjectDependencies(UIStatesController statesController)
        {
            _statesController = statesController;
        }

        public void SetTargetScene(SceneData sceneData)
        {
            _sceneData = sceneData;
        }        
        
        public async Task Load(Action<float> SetProgress)
        {
            int count = _sceneData.UIStatesRequest.UITypes.Count;
            Dictionary<UIType, UIState> states = new Dictionary<UIType, UIState>();

            float divider = 1.0f / count;

            SetProgress(0.0f);
            
            for (int i = 0; i < _sceneData.UIStatesRequest.UITypes.Count; i++)
            {
                ResourceRequest request = Resources.LoadAsync<UIState>(ResourcePaths.UI_STATE_PATHS[_sceneData.UIStatesRequest.UITypes[i]]);
                while (!request.isDone)
                {
                    await Task.Delay(1);
                }

                SetProgress(divider * i);
                var state = (UIState) request.asset;
                states.Add(_sceneData.UIStatesRequest.UITypes[i], state);
            }

            SetProgress(1.0f);
            _statesController.SetStates(states);
        }
    }
}
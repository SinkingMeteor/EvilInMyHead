using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sheldier.Common;
using Sheldier.Constants;
using Sheldier.UI;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Sheldier.Setup
{
    public class UILoadingOperation : ILoadOperation
    {
        private readonly UIStatesController _statesController;
        private readonly PersistUI _persistUI;
        private SceneData _sceneData;
        public string LoadLabel => "Loading UI";

        public UILoadingOperation(UIStatesController statesController, PersistUI persistUI)
        {
            _persistUI = persistUI;
            _statesController = statesController;
            _sceneData = ResourceLoader.Load<SceneData>(ResourcePaths.COLONY_SCENE_DATA_PATH);
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

            Fader fader = Resources.Load<Fader>(ResourcePaths.FADER);
            _persistUI.SetFader(Object.Instantiate(fader));
            RectTransform worldCanvas = Resources.Load<RectTransform>(ResourcePaths.WORLD_CANVAS);
            _persistUI.SetWorldCanvas(Object.Instantiate(worldCanvas));
            
            for (int i = 0; i < _sceneData.UIStatesRequest.UITypes.Count; i++)
            {
                ResourceRequest request = Resources.LoadAsync<UIState>(ResourcePaths.UI_STATE_PATHS[_sceneData.UIStatesRequest.UITypes[i]]);
                while (!request.isDone)
                {
                    await Task.Delay(1);
                }

                SetProgress(divider * i);
                var state = (UIState) request.asset;
                var instantiatedState = Object.Instantiate(state);
                instantiatedState.TurnOff();
                states.Add(_sceneData.UIStatesRequest.UITypes[i], instantiatedState);
            }

            
            SetProgress(1.0f);
            _statesController.SetStates(states);
        }
    }
}
using Sheldier.Installers;
using UnityEngine;
using Zenject;

namespace Sheldier.UI
{
    public class UISpawner
    {
       /* private UIInstaller _uiInstaller;
        private UIStatesController _uiStatesController;

        [Inject]
        private void InjectDependencies(UIInstaller uiInstaller, UIStatesController uiStatesController)
        {
            _uiStatesController = uiStatesController;
            _uiInstaller = uiInstaller;
        }

        public void SpawnUIStates()
        {
            GameObject uiMain = new GameObject("[UI]");
            uiMain.transform.position = Vector3.zero;
            
            foreach (var uiState in _uiStatesController.States.Values)
            {
                var state = GameObject.Instantiate(uiState, uiMain.transform, true);
                _uiInstaller.InjectUIState(state.gameObject);
            }
            _uiStatesController.Add(UIType.Inventory);
        }
        */
    }
}
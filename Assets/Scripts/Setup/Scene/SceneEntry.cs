using System;
using Sheldier.Actors;
using Sheldier.Common;
using Sheldier.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Sheldier.Setup
{
    public class SceneEntry : MonoBehaviour
    {
        [SerializeField] private Actor actor;
        
        private IInputProvider _inputProvider;
        private SceneLoadingOperation _sceneLoadingOperation;
        private GameGlobalSettings _globalSettings;

        [Inject]
        public void InjectDependencies(IInputProvider inputProvider, SceneLoadingOperation sceneLoadingOperation, GameGlobalSettings globalSettings)
        {
            _globalSettings = globalSettings;
            _sceneLoadingOperation = sceneLoadingOperation;
            _inputProvider = inputProvider;
        }

        private void Start()
        {
            CheckInitialScene();
            
            actor.Initialize();
        }

        private void CheckInitialScene()
        {
            if (_globalSettings.IsStarted)
                return;
            _sceneLoadingOperation.SetTargetScene(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(SceneNames.GAME_ENTRY);
        }
    }

}

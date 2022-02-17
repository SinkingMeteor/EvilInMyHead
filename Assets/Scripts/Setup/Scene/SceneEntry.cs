using Sheldier.Actors;
using Sheldier.Common;
using Sheldier.Constants;
using Sheldier.Factories;
using Sheldier.Gameplay.Effects;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Sheldier.Setup
{
    public class SceneEntry : MonoBehaviour
    {
        [SerializeField] private Actor actor;
        [SerializeField] private SceneCameraHandler sceneCameraHandler;
        
        
        private InputProvider _inputProvider;
        private SceneLoadingOperation _sceneLoadingOperation;
        private GameGlobalSettings _globalSettings;
        private ActorsEffectFactory _effectFactory;

        [Inject]
        public void InjectDependencies(InputProvider inputProvider, SceneLoadingOperation sceneLoadingOperation, GameGlobalSettings globalSettings,
            ActorsEffectFactory effectFactory)
        {
            _effectFactory = effectFactory;
            _globalSettings = globalSettings;
            _sceneLoadingOperation = sceneLoadingOperation;
            _inputProvider = inputProvider;
        }

        private void Start()
        {
            if (!GameIsInitialized())
                return;

            sceneCameraHandler.Initialize(_inputProvider);
            _inputProvider.SetSceneCamera(sceneCameraHandler.CurrentSceneCamera);
            sceneCameraHandler.SetFollowTarget(actor.transform);
            actor.Initialize();
            actor.ActorInputController.SetInputProvider(_inputProvider);
            actor.ActorEffectModule.AddEffect(_effectFactory.CreateEffect(ActorEffectType.Freeze));
        }

        private bool GameIsInitialized()
        {
            if (_globalSettings.IsStarted)
                return true;
            _sceneLoadingOperation.SetTargetScene(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(SceneNames.GAME_ENTRY);
            return false;
        }
    }

}

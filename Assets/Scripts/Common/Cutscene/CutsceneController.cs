using Sheldier.Actors;
using Sheldier.Actors.Pathfinding;
using Sheldier.Common.Pause;
using UnityEngine;

namespace Sheldier.Common.Cutscene
{
    public class CutsceneController
    {
        private ActorSpawner _actorSpawner;
        private PauseNotifier _pauseNotifier;
        private PathProvider _pathProvider;
        private ScenePlayerController _scenePlayerController;
        private Cutscene _currentCutscene;
        private DialoguesProvider _dialoguesProvider;

        public void SetDependencies(ActorSpawner actorSpawner, PauseNotifier pauseNotifier, PathProvider pathProvider, ScenePlayerController scenePlayerController,
            DialoguesProvider dialoguesProvider)
        {
            _dialoguesProvider = dialoguesProvider;
            _actorSpawner = actorSpawner;
            _pauseNotifier = pauseNotifier;
            _pathProvider = pathProvider;
            _scenePlayerController = scenePlayerController;
        }
        
        public void StartCutscene(string cutScenePath)
        {
            Cutscene cutscene = Resources.Load<Cutscene>(cutScenePath);
            if (ReferenceEquals(cutscene, null))
                return;
            _scenePlayerController.ControlledActor.LockInput();
            _currentCutscene = GameObject.Instantiate(cutscene);
            _currentCutscene.SetDependencies(_actorSpawner, _pauseNotifier, _pathProvider, _scenePlayerController, _dialoguesProvider);
            _currentCutscene.Play(OnCutsceneComplete);
        }

        private void OnCutsceneComplete()
        {
            _scenePlayerController.ControlledActor.UnlockInput();
            GameObject.Destroy(_currentCutscene);
        }
    }
}
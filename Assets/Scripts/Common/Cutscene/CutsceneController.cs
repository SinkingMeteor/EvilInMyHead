using Sheldier.Actors;
using Sheldier.Actors.Data;
using Sheldier.Actors.Pathfinding;
using Sheldier.Common.Pause;
using Sheldier.Data;
using UnityEngine;

namespace Sheldier.Common.Cutscene
{
    public class CutsceneController
    {
        private readonly Database<ActorDynamicConfigData> _dynamicConfigDatabase;
        private readonly SceneActorsDatabase _sceneActorsDatabase;

        private PauseNotifier _pauseNotifier;
        private PathProvider _pathProvider;
        private ScenePlayerController _scenePlayerController;
        private Cutscene _currentCutscene;
        private DialoguesProvider _dialoguesProvider;

        public CutsceneController(SceneActorsDatabase sceneActorsDatabase, PauseNotifier pauseNotifier, PathProvider pathProvider, ScenePlayerController scenePlayerController,
            DialoguesProvider dialoguesProvider, Database<ActorDynamicConfigData> dynamicConfigDatabase)
        {
            _dynamicConfigDatabase = dynamicConfigDatabase;
            _dialoguesProvider = dialoguesProvider;
            _sceneActorsDatabase = sceneActorsDatabase;
            _pauseNotifier = pauseNotifier;
            _pathProvider = pathProvider;
            _scenePlayerController = scenePlayerController;
        }
        
        public void StartCutscene(string cutScenePath)
        {
            Cutscene cutscene = Resources.Load<Cutscene>(cutScenePath);
            if (ReferenceEquals(cutscene, null))
                return;
            Actor controlledActor = GetCurrentPlayer();
            controlledActor.LockInput();
            _currentCutscene = GameObject.Instantiate(cutscene);
            _currentCutscene.SetDependencies(_sceneActorsDatabase, _pauseNotifier, _pathProvider, _dialoguesProvider, _dynamicConfigDatabase, controlledActor);
            _currentCutscene.Play(OnCutsceneComplete);
        }

        private void OnCutsceneComplete()
        {
            GetCurrentPlayer().UnlockInput();
            GameObject.Destroy(_currentCutscene);
        }

        private Actor GetCurrentPlayer()
        {
            var guid = _scenePlayerController.ControlledActorGuid;
            var dynamicData = _dynamicConfigDatabase.Get(guid);
            return _sceneActorsDatabase.Get(dynamicData.TypeName, guid);
        } 
    }
}
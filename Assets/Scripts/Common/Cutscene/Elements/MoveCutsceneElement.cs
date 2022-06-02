using System.Threading.Tasks;
using Sheldier.Actors;
using Sheldier.Actors.AI;
using Sheldier.Actors.Data;
using Sheldier.Actors.Pathfinding;
using Sheldier.Common.Asyncs;
using Sheldier.Common.Pause;
using Sheldier.Constants;
using Sheldier.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Sheldier.Common.Cutscene
{
    public class MoveCutsceneElement : SerializedMonoBehaviour, ICutsceneElement
    {
        public bool WaitElement => waitIt;
        
        [SerializeField] private Transform moveToPoint;
        [SerializeField] private DataReference actorToMove;
        [SerializeField] private bool waitIt;

        private bool _isFinished;
        
        private Database<ActorDynamicConfigData> _dynamicConfigDatabase;
        private ScenePlayerController _scenePlayerController;
        private SceneActorsDatabase _sceneActorsDatabase;
        private ActorsAIMoveModule _aiMoveModule;


        [Inject]
        private void InjectDependencies(Database<ActorDynamicConfigData> dynamicConfigDatabase,
                                        SceneActorsDatabase sceneActorsDatabase,
                                        ScenePlayerController scenePlayerController,
                                        PauseNotifier pauseNotifier,
                                        PathProvider pathProvider)
        {
            _sceneActorsDatabase = sceneActorsDatabase;
            _dynamicConfigDatabase = dynamicConfigDatabase;
            _scenePlayerController = scenePlayerController;
            _aiMoveModule = new ActorsAIMoveModule(pathProvider, pauseNotifier);
        }

        public async Task PlayCutScene()
        {
            string currentActorToMove = actorToMove.Reference; 
            if(!_dynamicConfigDatabase.IsItemExists(currentActorToMove))
                return;
            _isFinished = false;
            Actor actor = _sceneActorsDatabase.GetFirst(currentActorToMove);
            actor.AddExtraModule(_aiMoveModule);
            _aiMoveModule.MoveTo(moveToPoint, OnFinished);
            await AsyncWaitersFactory.WaitUntil(() => _isFinished);
            actor.RemoveExtraModule(_aiMoveModule);
        }

        private void OnFinished() => _isFinished = true;
    }
    
    public class ShakeCameraCutsceneElement : SerializedMonoBehaviour, ICutsceneElement
    {
        public bool WaitElement => waitIt;
        
        [SerializeField] private bool waitIt;
        [SerializeField] private float shakeDuration;

        public void SetDependencies(CutsceneInternalData actorSpawner)
        {
            
        }

        public async Task PlayCutScene()
        {
        }
    }
}
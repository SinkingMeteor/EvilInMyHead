using System.Collections;
using System.Threading.Tasks;
using Sheldier.Actors;
using Sheldier.Actors.AI;
using Sheldier.Common.Asyncs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Common.Cutscene
{
    public class MoveCutsceneElement : SerializedMonoBehaviour, ICutsceneElement
    {
        public bool WaitElement => waitIt;
        
        [SerializeField] private Transform moveToPoint;
        [SerializeField] private ActorType actorToMove;
        [SerializeField] private bool waitIt;

        private bool _isFinished;
        private CutsceneInternalData _data;

        public void SetDependencies(CutsceneInternalData data)
        {
            _data = data;
        }

        public async Task PlayCutScene()
        {
            ActorType currentActorToMove = actorToMove == ActorType.CurrentPlayer ? _data.CurrentPlayer.ActorType : actorToMove; 
            if(!_data.ActorSpawner.ActorsOnScene.ContainsKey(currentActorToMove))
                return;
            _isFinished = false;
            Actor actor = _data.ActorSpawner.ActorsOnScene[currentActorToMove][0];
            actor.AddExtraModule(_data.ActorsAIMoveModule);
            _data.ActorsAIMoveModule.MoveTo(moveToPoint, OnFinished);
            await AsyncWaitersFactory.WaitUntil(() => _isFinished);
            actor.RemoveExtraModule(_data.ActorsAIMoveModule);
        }

        private void OnFinished() => _isFinished = true;
    }
}
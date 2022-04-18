using System.Collections.Generic;
using Sheldier.Common;
using Sheldier.Common.Utilities;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Sheldier.Actors.Interact
{
    public class ActorsUpdatableInteractNotifier : SerializedMonoBehaviour, IExtraActorModule, ITickListener
    {
        [ReadOnly][OdinSerialize]private List<IInteractReceiver> _receivers;

        private ActorInputController _inputController;
        private CircleCollider2D _circleCollider2D;

        private Coroutine _inInteractField;

        private IInteractReceiver _currentReceiver;
        private ActorStateDataModule _stateData;
        private TickHandler _tickHandler;
        private Actor _actor;

        public void Initialize(ActorInternalData data)
        {
            _actor = data.Actor;
            _stateData = data.Actor.StateDataModule;
            _circleCollider2D = GetComponent<CircleCollider2D>();
            _inputController = data.Actor.InputController;
            _receivers = new List<IInteractReceiver>();
            _tickHandler = data.TickHandler;
            _tickHandler.AddListener(this);
            _inputController.OnUseButtonPressed += Interact;
        }
 
        public void Tick()
        {
            float minDistance = float.MaxValue;
            IInteractReceiver nextReceiver = default;
            for (int i = 0; i < _receivers.Count; i++)
            {
                LengthCheckResult checkResult = IsInsideField(_receivers[i]);
                
                if (!checkResult.IsInside)
                {
                    var lastIndex = _receivers.Count - 1;
                    _receivers[i].OnExit();
                    if (ReferenceEquals(_receivers[i], _currentReceiver))
                        _currentReceiver = null;
                    (_receivers[i], _receivers[^1]) = (_receivers[^1], _receivers[i]);
                    _receivers.RemoveAt(lastIndex);
                    i -= 1;
                    continue;
                }

                if (checkResult.Distance < minDistance)
                {
                    minDistance = checkResult.Distance;
                    nextReceiver = _receivers[i];
                }
            }
            
            if(!ReferenceEquals(nextReceiver, _currentReceiver) && !ReferenceEquals(nextReceiver, null))
                EnterNewReceiver(nextReceiver);
        }
        public void Dispose()
        {
            _tickHandler.RemoveListener(this);
            _inputController.OnUseButtonPressed -= Interact;
        }
        private void Interact()
        {
            if (ReferenceEquals(_currentReceiver, null))
                return;
            _currentReceiver.OnInteracted(_actor);
            _actor.Notifier.NotifyInteracting(_currentReceiver);
        }
        private LengthCheckResult IsInsideField(IInteractReceiver interactReceiver)
        {
            var length = (interactReceiver.ColliderPosition - _actor.transform.position.DiscardZ()).magnitude;
            return new LengthCheckResult()
            {
                Distance = length,
                IsInside = length < _circleCollider2D.radius + interactReceiver.ColliderSize
            };
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.TryGetComponent(out IInteractReceiver receiver))
            {
                if (_receivers.Contains(receiver))
                    return;
                AddNewReceiver(receiver);
            }
        }
        private void AddNewReceiver(IInteractReceiver interactReceiver)
        {
            _receivers.Add(interactReceiver);
            EnterNewReceiver(interactReceiver);
        }
        private void EnterNewReceiver(IInteractReceiver interactReceiver)
        {
            if (!ReferenceEquals(_currentReceiver, null))
            {
                LengthCheckResult currentResult = IsInsideField(_currentReceiver);
                LengthCheckResult newResult = IsInsideField(interactReceiver);
                if (currentResult.Distance > newResult.Distance)
                {
                    _currentReceiver.OnExit();
                    _currentReceiver = interactReceiver;
                    _currentReceiver.OnEntered();
                }
                return;
            }
            
            _currentReceiver = interactReceiver;
            _currentReceiver.OnEntered();
        }
        private void OnDrawGizmos()
        {
            if (_currentReceiver == null) return;
            
            Gizmos.DrawLine(transform.position, _currentReceiver.Transform.position);
        }

        private struct LengthCheckResult
        {
            public float Distance;
            public bool IsInside;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using Sheldier.Setup;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Sheldier.Actors.Interact
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class ActorsInteractModule : SerializedMonoBehaviour, IExtraActorModule
    {
        public int Priority => 0;
        
        [SerializeField] private CircleCollider2D circleCollider2D;
        [ReadOnly][OdinSerialize]private Stack<IInteractReceiver> _receivers;

        private ActorNotifyModule _notifier;
        private ActorInputController _inputController;

        private Coroutine _inInteractField;

        private IInteractReceiver _currentReceiver;
        private Actor _actor;

        public void Initialize(ActorInternalData data)
        {
            _notifier = data.Actor.Notifier;
            _actor = data.Actor;
            _inputController = data.Actor.InputController;
            _receivers = new Stack<IInteractReceiver>();
            _inputController.OnUseButtonPressed += Interact;
            _actor.OnAddedControl += OnSetInput;
        }

        private void OnSetInput()
        {
            if(_currentReceiver != null)
                CheckInput(_currentReceiver);
        }

        private bool IsInsideField(Transform receiver)
        {
            var length = (receiver.position - transform.position).magnitude - 0.4f;
            return length < circleCollider2D.radius;
        }

        private void Interact()
        {
            if(_currentReceiver == null)
                return;
            _currentReceiver.OnInteracted(_actor);
            if(_inInteractField != null)
                StopCoroutine(_inInteractField);
            _currentReceiver.OnExit();
            PopOldReceiver();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.TryGetComponent(out IInteractReceiver receiver))
            {
                CheckInput(receiver);
            }
        }

        private void CheckInput(IInteractReceiver receiver)
        {
            if (_currentReceiver != null)
                PushOldReceiver();
            EnterReceiver(_receivers.Contains(receiver) ? GetPushedReceiver(receiver) : receiver);
        }

        private void PushOldReceiver()
        {
            if(!_receivers.Contains(_currentReceiver))
                _receivers.Push(_currentReceiver);
            DisposeReceiver();
        }

        private void PopOldReceiver()
        {
            if (_receivers.Count == 0)
                return;

            var lastReceiver = _receivers.Pop();
            if (!IsInsideField(lastReceiver.Transform))
                PopOldReceiver();
            else
                EnterReceiver(lastReceiver);
        }

        private IInteractReceiver GetPushedReceiver(IInteractReceiver receiverToExtract)
        {
            List<IInteractReceiver> popedReceivers = new List<IInteractReceiver>();
            IInteractReceiver foundReceiver = null;
            while (_receivers.Count != 0)
            {
                foundReceiver = _receivers.Pop();
                if(foundReceiver == receiverToExtract)
                    break;
                popedReceivers.Add(foundReceiver);
            }

            for(int i = popedReceivers.Count - 1; i >= 0; i--)
                _receivers.Push(popedReceivers[i]);

            return foundReceiver;
        }
        private void EnterReceiver(IInteractReceiver receiver)
        {
            _currentReceiver = receiver;
            _currentReceiver.OnEntered();
            _inInteractField = StartCoroutine(CheckDistanceCoroutine());
        }
        
        private IEnumerator CheckDistanceCoroutine()
        {
            while (IsInsideField(_currentReceiver.Transform))
            {
                yield return null;
            }
            DisposeReceiver();
            PopOldReceiver();
        }
        private void DisposeReceiver()
        {
            if(_inInteractField != null)
                StopCoroutine(_inInteractField);
            _currentReceiver.OnExit();
            _currentReceiver = null;
        }

        private void OnDrawGizmos()
        {
            if (_currentReceiver == null) return;
            
            Gizmos.DrawLine(transform.position, _currentReceiver.Transform.position);
        }

        public void Dispose()
        {
            _actor.OnAddedControl -= OnSetInput;
            _inputController.OnUseButtonPressed -= Interact;

        }
    }
}

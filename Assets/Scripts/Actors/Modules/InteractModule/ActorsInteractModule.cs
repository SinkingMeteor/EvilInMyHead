using System.Collections;
using System.Collections.Generic;
using Sheldier.Setup;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Actors.Interact
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class ActorsInteractModule : SerializedMonoBehaviour, IExtraActorModule
    {
        public int Priority => 0;
        
        [SerializeField] private CircleCollider2D circleCollider2D;

        private ActorNotifyModule _notifier;
        private ActorInputController _inputController;

        private Coroutine _inInteractField;

        private IInteractReceiver _currentReceiver;
        private Stack<IInteractReceiver> _receivers;

        public void Initialize(ActorInternalData data)
        {
            _notifier = data.Notifier;
            _inputController = data.ActorInputController;
            _receivers = new Stack<IInteractReceiver>();
            _inputController.OnUseButtonPressed += Interact;
        }
        
        private bool IsInsideField(Transform receiver)
        {
            var length = (receiver.position - transform.position).magnitude - 0.2f;
            return length < circleCollider2D.radius;
        }

        private void Interact()
        {
            if(_currentReceiver == null)
                return;
            _currentReceiver.OnInteracted(_notifier);
            DisposeReceiver();
            PopOldReceiver();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.TryGetComponent(out IInteractReceiver receiver))
            {
                Debug.Log("Found");
                if(_currentReceiver != null)
                    PushOldReceiver();
                EnterReceiver(_receivers.Contains(receiver) ? GetPushedReceiver(receiver) : receiver);
            }
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

        private void OnDestroy()
        {
            #if UNITY_EDITOR
            if (!GameGlobalSettings.IsStarted) return;
            #endif
            _inputController.OnUseButtonPressed -= Interact;

        }
    }
}

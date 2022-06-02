using System;
using System.Collections.Generic;
using Sheldier.Common;
using Sheldier.Common.Pool;
using UniRx;
using UnityEngine;

namespace Sheldier.UI
{
    public class InteractHintController
    {
        private readonly PersistUI _persistUI;
        private readonly IPool<InteractHint> _interactHintPool;
        private readonly ScenePlayerController _scenePlayerController;
        private readonly IInputBindIconProvider _inputBindIconProvider;
        
        private IDisposable _interactHintEvent;
        private InteractHint _currentHint;

        public InteractHintController(PersistUI persistUI,
                                      ScenePlayerController scenePlayerController,
                                      IPool<InteractHint> interactHintPool,
                                      IInputBindIconProvider inputBindIconProvider)
        {
            _inputBindIconProvider = inputBindIconProvider;
            _interactHintPool = interactHintPool;
            _persistUI = persistUI;
            _scenePlayerController = scenePlayerController;
        }

        public void Initialize()
        {
            _interactHintEvent = MessageBroker.Default.Receive<InteractHintRequest>().Subscribe(OnInteractHintRequestReceived);
        }

        private void OnInteractHintRequestReceived(InteractHintRequest interactHintRequest)
        {
            if (!interactHintRequest.Activate)
            {
                _currentHint.Close();
                _currentHint = null;
                return;
            }
                    
            _currentHint = _interactHintPool.GetFromPool();
            _currentHint.transform.SetParent(_persistUI.WorldCanvasRectTransform);
            _currentHint.transform.position = interactHintRequest.HintPosition;
            _currentHint.SetImage(_inputBindIconProvider.GetActionInputSprite(interactHintRequest.ActionType));
        }
        
        public void Dispose()
        {
            _interactHintEvent.Dispose();
        }
    }

    public struct InteractHintRequest
    {
        public bool Activate;
        public InputActionType ActionType;
        public Vector2 HintPosition;
    }
}


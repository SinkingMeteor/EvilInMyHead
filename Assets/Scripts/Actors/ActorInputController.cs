using System;
using Sheldier.Common;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorInputController
    {
        public Vector2 MovementDirection => _currentInputProvider.MovementDirection;
        public Vector2 CursorScreenDirection => _currentInputProvider.CursorScreenDirection;

        public event Action OnUseButtonPressed; 
        public event Action OnUseButtonReleased; 
        public event Action OnAttackButtonPressed; 
        public event Action OnAttackButtonReleased; 
        public event Action OnReloadButtonPressed; 
        public event Action OnReloadButtonReleased; 
        
        public IInputProvider CurrentInputProvider => _currentInputProvider;

        private IInputProvider _currentInputProvider;
        private IInputProvider _nullProvider;
        private IInputProvider _realInputProvider;
        

        public void Initialize()
        {
            _nullProvider = new NullInputProvider();
            _currentInputProvider = _nullProvider;
        }
        public void SetInputProvider(IInputProvider inputProvider)
        {
            _realInputProvider = inputProvider;
            _currentInputProvider = _realInputProvider;

            _realInputProvider.UseButton.OnPressed += UseButtonPressed;
            _realInputProvider.UseButton.OnReleased += UseButtonReleased;
            _realInputProvider.AttackButton.OnPressed += AttackButtonPressed;
            _realInputProvider.AttackButton.OnReleased += AttackButtonReleased;
            _realInputProvider.ReloadButton.OnPressed += ReloadButtonPressed;
            _realInputProvider.ReloadButton.OnReleased += ReloadButtonReleased;

        }
        public void RemoveInputProvider()
        {
            _realInputProvider.UseButton.OnPressed -= UseButtonPressed;
            _realInputProvider.UseButton.OnReleased -= UseButtonReleased;
            _realInputProvider.AttackButton.OnPressed -= AttackButtonPressed;
            _realInputProvider.AttackButton.OnReleased -= AttackButtonReleased;
            _realInputProvider.ReloadButton.OnPressed -= ReloadButtonPressed;
            _realInputProvider.ReloadButton.OnReleased -= ReloadButtonReleased;
            
            _realInputProvider = null;
            _currentInputProvider = _nullProvider;
        }
        public void LockInput()
        {
            _currentInputProvider = _nullProvider;
        }

        public void UnlockInput()
        {
            _currentInputProvider = _realInputProvider;
        }
        
        private void UseButtonReleased() => OnUseButtonReleased?.Invoke();
        private void UseButtonPressed() => OnUseButtonPressed?.Invoke();
        private void AttackButtonReleased() => OnAttackButtonReleased?.Invoke();
        private void AttackButtonPressed() => OnAttackButtonPressed?.Invoke();
        private void ReloadButtonReleased() => OnReloadButtonReleased?.Invoke();
        private void ReloadButtonPressed() => OnReloadButtonPressed?.Invoke();
    }
}
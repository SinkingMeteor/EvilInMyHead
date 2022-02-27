using System;
using Sheldier.Common;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorInputController
    {
        public Vector2 MovementDirection => _currentInputProvider.MovementDirection;
        public Vector2 CursorScreenDirection => _currentInputProvider.CursorScreenCenterDirection;
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
        public Vector2 GetNonNormalizedCursorDirectionByTransform(Vector3 position) => _currentInputProvider.GetNonNormalizedDirectionToCursorFromPosition(position);
        
        public void SetInputProvider(IInputProvider inputProvider)
        {

            _currentInputProvider = inputProvider;

            _currentInputProvider.UseButton.OnPressed += UseButtonPressed;
            _currentInputProvider.UseButton.OnReleased += UseButtonReleased;
            _currentInputProvider.AttackButton.OnPressed += AttackButtonPressed;
            _currentInputProvider.AttackButton.OnReleased += AttackButtonReleased;
            _currentInputProvider.ReloadButton.OnPressed += ReloadButtonPressed;
            _currentInputProvider.ReloadButton.OnReleased += ReloadButtonReleased;

        }
        public void RemoveInputProvider()
        {
            _currentInputProvider.UseButton.OnPressed -= UseButtonPressed;
            _currentInputProvider.UseButton.OnReleased -= UseButtonReleased;
            _currentInputProvider.AttackButton.OnPressed -= AttackButtonPressed;
            _currentInputProvider.AttackButton.OnReleased -= AttackButtonReleased;
            _currentInputProvider.ReloadButton.OnPressed -= ReloadButtonPressed;
            _currentInputProvider.ReloadButton.OnReleased -= ReloadButtonReleased;
            
            _currentInputProvider = _nullProvider;
        }
        public void LockInput()
        {
            _realInputProvider = _currentInputProvider;
            _currentInputProvider = _nullProvider;
        }

        public void UnlockInput()
        {
            _currentInputProvider = _realInputProvider;
            _realInputProvider = null;
        }
        
        private void UseButtonReleased() => OnUseButtonReleased?.Invoke();
        private void UseButtonPressed() => OnUseButtonPressed?.Invoke();
        private void AttackButtonReleased() => OnAttackButtonReleased?.Invoke();
        private void AttackButtonPressed() => OnAttackButtonPressed?.Invoke();
        private void ReloadButtonReleased() => OnReloadButtonReleased?.Invoke();
        private void ReloadButtonPressed() => OnReloadButtonPressed?.Invoke();
    }
}
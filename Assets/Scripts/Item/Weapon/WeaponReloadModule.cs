using System;
using System.Collections;
using Sheldier.Actors.Hand;
using UnityEngine;

namespace Sheldier.Item
{
    public class WeaponReloadModule
    {
        public bool CanShoot => !_isReloading;
        public bool IsReloading => _isReloading;
        
        private readonly WeaponConfig _weaponConfig;
        private Coroutine _reloadingCoroutine;
        private HandView _weaponView;
        private bool _isReloading;
        private bool _canShoot;

        public WeaponReloadModule(WeaponConfig weaponConfig)
        {
            _weaponConfig = weaponConfig;
        }

        public void SetView(HandView handView)
        {
            _weaponView = handView;
        }

        public void StartReloading(Action onComplete) => _reloadingCoroutine = _weaponView.StartCoroutine(ReloadingCoroutine(onComplete));

        public void InterruptReloading()
        {
            if (_reloadingCoroutine == null) return;
            
            _weaponView.StopCoroutine(_reloadingCoroutine);
            _weaponView.Animator.StopPlaying();
        }
        private IEnumerator ReloadingCoroutine(Action onComplete)
        {
            _isReloading = true;
            
            _weaponView.Animator.Play(_weaponConfig.ReloadAnimation);

            yield return new WaitForSeconds(_weaponConfig.ReloadAnimation.Frames.Length / _weaponConfig.ReloadAnimation.FrameRate);

            _isReloading = false;
            Debug.Log("Reloaded");

            onComplete.Invoke();
        }

        public void Dispose()
        {
            InterruptReloading();
            _weaponView = null;
        }
    }
}
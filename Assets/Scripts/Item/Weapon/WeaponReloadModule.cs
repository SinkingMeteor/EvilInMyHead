using System;
using System.Collections;
using Sheldier.Actors.Hand;
using Sheldier.Common.Animation;
using Sheldier.Constants;
using Sheldier.Data;
using UnityEngine;

namespace Sheldier.Item
{
    public class WeaponReloadModule
    {
        public bool CanShoot => !_isReloading;
        public bool IsReloading => _isReloading;

        private readonly AssetProvider<AnimationData> _animationLoader;
        
        private ItemDynamicWeaponData _weaponConfig;
        private Coroutine _reloadingCoroutine;
        private IHandView _weaponView;
        private bool _isReloading;
        private bool _canShoot;

        public WeaponReloadModule(ItemDynamicWeaponData weaponData, AssetProvider<AnimationData> animationLoader)
        {
            _animationLoader = animationLoader;
            _weaponConfig = weaponData;
        }
        public void SetView(IHandView handView)
        {
            _weaponView = handView;
        }
        public void StartReloading(Action onComplete) => _reloadingCoroutine = _weaponView.Behaviour.StartCoroutine(ReloadingCoroutine(onComplete));

        public void InterruptReloading()
        {
            if (_reloadingCoroutine == null) return;
            
            _weaponView.Behaviour.StopCoroutine(_reloadingCoroutine);
            _weaponView.Animator.StopPlaying();
        }
        private IEnumerator ReloadingCoroutine(Action onComplete)
        {
            _isReloading = true;
            AnimationData animationData = _animationLoader.Get(_weaponConfig.ReloadAnimation);
            _weaponView.Animator.Play();

            yield return new WaitForSeconds(animationData.Frames.Length / animationData.FrameRate);

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
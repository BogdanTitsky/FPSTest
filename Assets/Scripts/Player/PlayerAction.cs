using System;
using Animation;
using Guns;
using UnityEngine;
using UnityEngine.UIElements;

namespace Player
{
    [DisallowMultipleComponent]
    public class PlayerAction : MonoBehaviour
    {
        [SerializeField] private PlayerGunSelector gunSelector;
        [SerializeField] private bool autoReload = true;
        [SerializeField] private WeaponReloadAnimator weaponReloadAnimator;
        public event Action OnReload;

        private bool _isReloading;
        
        private void OnEnable()
        {
            if (weaponReloadAnimator != null)
            {
                weaponReloadAnimator.OnReloadComplete += EndReload;
            }
        }

        private void OnDisable()
        {
            if (weaponReloadAnimator != null)
            {
                weaponReloadAnimator.OnReloadComplete -= EndReload;
            }
        }
        
        private void Update()
        {
            if (gunSelector != null && gunSelector.activeGun != null)
            {
                gunSelector.activeGun.TryToShoot(Input.GetMouseButton(0));
            }

            if (ShouldManualReload() || ShouldAutoReload())
            {
                gunSelector.activeGun.StartReloading();
                _isReloading = true;
                OnReload?.Invoke();
            }
            
        }

        private void EndReload()
        {
            gunSelector.activeGun.EndReload();
            _isReloading = false;
        }

        private bool ShouldManualReload()
        {
            return !_isReloading && Input.GetKeyDown(KeyCode.R) && gunSelector.activeGun.CanReload();
        }

        private bool ShouldAutoReload()
        {
            if (gunSelector.activeGun == null)
                return false;
            return !_isReloading && autoReload && gunSelector.activeGun.ammoConfig.currentClipAmmo == 0  &&
                   gunSelector.activeGun.CanReload();
        }
    }
}
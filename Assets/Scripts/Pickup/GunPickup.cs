using System;
using Guns;
using UnityEngine;

namespace Pickup
{
    [RequireComponent(typeof(Collider))]
    public class GunPickup : MonoBehaviour
    {
        [SerializeField] private float weaponResetCooldown = 5;
        [SerializeField] private GameObject weapon;
        
        public GunScriptableObject gun;
        private Vector3 SpinDirection = Vector3.up;
        private float cooldownTimer;

        private void Start()
        {
            cooldownTimer = weaponResetCooldown;
        }

        private void ResetAmmoConfig()
        {
            gun.ammoConfig.currentClipAmmo = gun.ammoConfig.clipSize;
            gun.ammoConfig.currentAmmo = gun.ammoConfig.maxAmmo;
        }

        private void Update()
        {
            cooldownTimer += 1 * Time.deltaTime;
            transform.Rotate(SpinDirection);
            weapon.SetActive(cooldownTimer >= weaponResetCooldown);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerGunSelector gunSelector))
            {            
                ResetAmmoConfig();
                cooldownTimer = 0;
                gunSelector.PickupGun(gun);
            }
        }
    }
}

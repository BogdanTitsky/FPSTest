using System;
using UnityEngine;

namespace Guns
{
    [CreateAssetMenu(fileName = "Ammo Config", menuName = "Guns/Ammo Config", order = 3)]
    public class AmmoConfigScriptableObject : ScriptableObject
    {
        [SerializeField] private bool resetAmmo;
        
        public int maxAmmo = 120;
        public int clipSize = 30;

        public int currentAmmo = 120;
        public int currentClipAmmo = 30;

        private void OnEnable()
        {
            if (resetAmmo)
            {
                currentAmmo = maxAmmo;
                currentClipAmmo = clipSize;
                
            }
        }

        public void Reload()
        {
            int maxReloadAmount = Mathf.Min(clipSize, currentAmmo);
            int availableBulletsInCurrentClip = clipSize - currentClipAmmo;
            int reloadAmount = Mathf.Min(maxReloadAmount, availableBulletsInCurrentClip);
            currentClipAmmo += reloadAmount;
            currentAmmo -= reloadAmount;
        }
        
        public bool CanReload()
        {
            return currentClipAmmo < clipSize && currentAmmo > 0;
        }
    }
}

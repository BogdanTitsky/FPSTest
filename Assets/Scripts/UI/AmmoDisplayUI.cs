using System;
using Guns;
using TMPro;
using UnityEngine;

namespace UI
{
    public class AmmoDisplayUI : MonoBehaviour
    {
        [SerializeField] private PlayerGunSelector gunSelector;

        [SerializeField] private TextMeshProUGUI ammoText;

        private void Update()
        {
            SetAmmoText();
        }


        private void SetAmmoText()
        {
            if (gunSelector.activeGun != null)            
                ammoText.SetText($"{gunSelector.activeGun.ammoConfig.currentClipAmmo} / {gunSelector.activeGun.ammoConfig.currentAmmo}");
            else
                ammoText.SetText("0 / 0");
        }
    }
}

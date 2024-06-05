using System;
using System.Collections.Generic;
using Guns.Enum;
using UnityEngine;

namespace Guns
{
    [DisallowMultipleComponent]
    public class PlayerGunSelector: MonoBehaviour
    {
        [SerializeField] private EGunType eGun;
        [SerializeField] private Transform gunParent;
        [SerializeField] private int maxInvertorySlots = 3;
        
        public List<GunScriptableObject> guns;
         public int activeGunIndex;
        [Space] [Header("Runtime filled")] public GunScriptableObject activeGun;
        
        public event Action<List<GunScriptableObject>> OnGunListChanged;
        public event Action<int> OnActiveSlotChanged;
        private void Start()
        {
            foreach (GunScriptableObject gun in guns)
            {
                SetupGun(gun);
            }

            OnGunListChanged?.Invoke(guns);
            SelectGunByIndex(0);
        }

        private void SetupGun(GunScriptableObject gun)
        {
            gun.Spawn(gunParent, this);
            if (gun._model != null)
                gun._model.SetActive(false); 
            else
                Debug.LogError($"Gun model not found for gun type: {gun.type}");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SelectGunByIndex(0);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                SelectGunByIndex(1);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                SelectGunByIndex(2);
            if (Input.GetKeyDown(KeyCode.Q))
                DropCurrentGun();
        }

        private void SelectGunByIndex(int index)
        {
            if (index < 0 || index >= guns.Count)
                return;

            if (activeGun != null && activeGun._model != null)
                activeGun._model.SetActive(false);

            activeGun = guns[index];

            if (activeGun != null && activeGun._model != null)
            {
                activeGunIndex = index;
                activeGun._model.SetActive(true);
                OnActiveSlotChanged?.Invoke(index);
            }
            else
                Debug.LogError($"Active gun or its model not found for index: {index}");
        }

        public void PickupGun(GunScriptableObject gun)
        {
            if (guns.Count == maxInvertorySlots) return;
            SetupGun(gun);
            guns.Add(gun);
            UpdateGunList(guns);
        }
        
        private void UpdateGunList(List<GunScriptableObject> newGunList)
        {
            guns = newGunList;
            OnGunListChanged?.Invoke(guns);
        }

        private void DropCurrentGun()
        {
            if (guns.Count == 0) return;
            
            activeGun._model.SetActive(false);
            guns.RemoveAt(activeGunIndex);
            OnGunListChanged?.Invoke(guns);
            
            if (guns.Count > 0)
            {
                int newIndex = activeGunIndex % guns.Count;
                SelectGunByIndex(newIndex);
            }
            else
            {
                OnActiveSlotChanged?.Invoke(0);
                activeGun = null;
                activeGunIndex = -1;
            }
        }
    }
}

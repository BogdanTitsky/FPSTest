using System.Collections.Generic;
using Guns;
using UnityEngine;
using UnityEngine.UI;

public class GunSelectorDisplayUI : MonoBehaviour
{
    [SerializeField] private PlayerGunSelector gunSelector;
    [SerializeField] private List<Image> gunSlots; 
    [SerializeField] private List<Image> frames; 

    private void OnEnable()
    {
        if (gunSelector != null)
        {
            gunSelector.OnGunListChanged += UpdateGunSlots;
            gunSelector.OnActiveSlotChanged += HighlightActiveSlot;
        }

        UpdateGunSlots(gunSelector.guns);
    }

    private void OnDisable()
    {
        if (gunSelector != null)
        {
            gunSelector.OnGunListChanged -= UpdateGunSlots;
            gunSelector.OnActiveSlotChanged -= HighlightActiveSlot;
        }
    }

    private void UpdateGunSlots(List<GunScriptableObject> guns)
    {
        for (int i = 0; i < gunSlots.Count; i++)
        {
            if (i < guns.Count && guns[i].gunIcon != null)
            {
                gunSlots[i].sprite = guns[i].gunIcon;
                gunSlots[i].gameObject.SetActive(true);
            }
            else
            {
                gunSlots[i].gameObject.SetActive(false);
            }
        }
    }

    private void HighlightActiveSlot(int index)
    {                
        foreach (var frame in frames)
        {
            frame.enabled = false;
        }

        if (gunSelector.guns.Count == 0)
        {
            return;
        }
        frames[index].enabled = true;
    }
}

using Enemy;
using Guns;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerHpDisplayUI : MonoBehaviour
    {
        [SerializeField] private Health playerHp;

        [SerializeField] private TextMeshProUGUI hp;
        
      private  void Update()
        {
            hp.SetText($"{playerHp.CurrentHealth} / {playerHp.MaxHealth}");
        }
    }
}

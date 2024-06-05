using TMPro;
using UnityEngine;

namespace UI
{
    public class WinLoseDisplayUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        private void OnEnable()
        {
            string textInfo = $"Wins: {PlayerStats.Instance.Wins.ToString()} / Loses: {PlayerStats.Instance.Losses.ToString()}";
            text.text = textInfo;
        }
    }
}

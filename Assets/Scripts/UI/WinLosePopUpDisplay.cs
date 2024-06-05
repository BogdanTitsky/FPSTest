using TMPro;
using UnityEngine;

namespace UI
{
    public class WinLosePopUpDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI winLosePopupText;
        
        public void ShowPopup(string message)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; 
            
            winLosePopupText.text = message;
            Time.timeScale = 0;
            gameObject.SetActive(true);
        }
        public void HidePopup()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false; 
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
        
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common
{
    public class SceneController : MonoBehaviour
    {
        public void LoadGameScene()
        {
            SceneManager.LoadScene("GameScene");
            Time.timeScale = 1;
        }

        public void LoadMenuScene()
        {
            SceneManager.LoadScene("MenuScene");
            Time.timeScale = 1;
        }

        public void ReloadCurrentScene()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
            Time.timeScale = 1;
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
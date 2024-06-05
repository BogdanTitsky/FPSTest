using System;
using Enemy;
using TMPro;
using UI;
using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Health playerHealth;
        [SerializeField] private Health[] enemiesHealth;
        [SerializeField] private WinLosePopUpDisplay winLosePopup;
        
        private int enemiesKilled;

        private void OnEnable()
        {
            playerHealth.OnDeath += GameLose;
            foreach (var enemyHealth in enemiesHealth)
            {
                enemyHealth.OnDeath += OnEnemyDeath;
            }
        }
        
        private void OnDisable()
        {
            playerHealth.OnDeath -= GameLose;
            foreach (var enemyHealth in enemiesHealth)
            {
                enemyHealth.OnDeath -= OnEnemyDeath;
            }
        }
        
        private void OnEnemyDeath(Vector3 position)
        {
            enemiesKilled++;
            if (enemiesKilled >= enemiesHealth.Length)
            {
                GameWin();
            }
        }
        
        private void GameLose(Vector3 position)
        {
            winLosePopup.ShowPopup("You lose");
            PlayerStats.Instance.IncrementLosses();
        }
        
        
        private void GameWin()
        {
            winLosePopup.ShowPopup("You win");
            PlayerStats.Instance.IncrementWins(); 
        }
        
       
        
    }
}
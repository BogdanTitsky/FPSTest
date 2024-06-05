using UnityEngine;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private Health playerHealth;
        [SerializeField] private EnemyMovement enemyMover;
        [SerializeField] private int damage;
        

        private void OnEnable()
        {
            enemyMover.OnAttackPlayer += Attack;
        }

        private void OnDisable()
        {
            enemyMover.OnAttackPlayer -= Attack;
        }
        private void Attack()
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
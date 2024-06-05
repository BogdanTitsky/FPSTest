using System;
using UnityEngine;

namespace Enemy
{
    [DisallowMultipleComponent]
    public class Enemy : MonoBehaviour
    {        
        [SerializeField] private Animator animator;

        public Health health;
        public EnemyPainResponse PainResponse;

        private void OnEnable()
        {
            health.OnDeath += Die;
        }

        private void OnDisable()
        {
            health.OnDeath -= Die;
        }

        private void Die(Vector3 position)
        {
            animator.SetTrigger("Dead");
        }

    }
}
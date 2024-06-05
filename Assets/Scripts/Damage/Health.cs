using System;
using Damage;
using UnityEngine;

namespace Enemy
{
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int health; //just for checking and debugging
        
        public int CurrentHealth
        {
            get => health; private set => health = value;
        }

        public int MaxHealth
        {
            get => maxHealth; private set => maxHealth = value;
        }
        
        public event IDamageable.TakeDamageEvent OnTakeDamage;
        public event IDamageable.DeathEvent OnDeath;

        private void OnEnable()
        {
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(int damage)
        {
           int damageTaken = Mathf.Clamp(damage, 0, CurrentHealth);
           
           CurrentHealth -= damageTaken;

           if (damageTaken != 0)
           {
               OnTakeDamage?.Invoke(damageTaken);
           }

           if (CurrentHealth == 0 && damageTaken !=0)
           {
               OnDeath?.Invoke(transform.position);
           }
        }
    }
}
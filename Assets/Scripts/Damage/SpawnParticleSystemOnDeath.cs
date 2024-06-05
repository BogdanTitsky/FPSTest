using UnityEngine;

namespace Damage
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(IDamageable))]
    public class SpawnParticleSystemOnDeath : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem deathSystem;

        private IDamageable _damageable;

        private void Awake()
        {
            _damageable = GetComponent<IDamageable>();
        }

        private void OnEnable()
        {
            _damageable.OnDeath += Damageable_OnDeath;
        }

        private void Damageable_OnDeath(Vector3 position)
        {
            Instantiate(deathSystem, position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
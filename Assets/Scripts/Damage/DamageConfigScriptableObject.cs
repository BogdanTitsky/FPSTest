using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace Damage
{
    [CreateAssetMenu(fileName = "Damage config", menuName = "Guns/Damage config", order = 6)]
    public class DamageConfigScriptableObject : ScriptableObject
    {
        public MinMaxCurve damageCurve;

        private void Reset()
        {
            damageCurve.mode = ParticleSystemCurveMode.Curve;
        }

        public int GetDamage(float distance = 0)
        {
            return Mathf.CeilToInt(damageCurve.Evaluate(distance, Random.value));
        }    
    }
}
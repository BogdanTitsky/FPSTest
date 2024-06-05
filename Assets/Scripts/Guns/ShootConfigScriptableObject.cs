using Guns.Enum;
using UnityEngine;
using UnityEngine.Serialization;

namespace Guns
{
    [CreateAssetMenu(fileName = "Shoot Config", menuName = "Guns/Shoot Config", order = 2)]
    public class ShootConfigScriptableObject : ScriptableObject
    {
        public LayerMask hitMask;
        public Vector3 spread = new (0.1f, 0.1f, 0.1f);
        public float fireRate = 0.25f;
        public float recoilRecoverySpeed = 2f;
        public float maxSpreadTime = 1f;
        
        public ESpreadType spreadType = ESpreadType.Normal;
        

        public Vector3 GetSpread(float shootTime = 0f)
        {
            Vector3 returnedSpread = Vector3.zero;
            if (spreadType == ESpreadType.Normal)
            {
                returnedSpread = Vector3.Lerp(Vector3.zero, new Vector3(
                    Random.Range(-spread.x, spread.x),
                    Random.Range(-spread.y, spread.y),
                    Random.Range(-spread.z, spread.z)),
                 Mathf.Clamp01(shootTime / maxSpreadTime));
            }

            return returnedSpread; 
        }
    }
}
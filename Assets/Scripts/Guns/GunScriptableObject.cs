using System.Collections;
using Audio;
using Damage;
using Guns.Enum;
using UnityEngine;
using UnityEngine.Pool;

namespace Guns
{
    [CreateAssetMenu(fileName = "Gun", menuName = "Guns/Gun", order = 0)]
    public class GunScriptableObject : ScriptableObject
    {
        public EGunType type;    
        public string Name; 
        public GameObject modelPrefab; 
        public Vector3 spawnPoint; 
        public Vector3 spawnRotation;

        public DamageConfigScriptableObject damageConfig;
        public ShootConfigScriptableObject shootConfig;
        public TrailConfigScriptableObject trailConfig;
        public AmmoConfigScriptableObject ammoConfig;
        public GunAudioConfigScriptableObject gunAudioConfig;
        public GameObject _model;
        public Sprite gunIcon;

        private MonoBehaviour _activeMonoBehaviour;
        private AudioSource _audioSource;
        
        private float _lastShootTime, _initialClickTime, _shootingStopTime;
        private bool _lastFrameFireBtnClicked;
        private ParticleSystem _shootSystem;
        private ObjectPool<TrailRenderer> _trailPool;

        public void Spawn(Transform parent, MonoBehaviour newActiveMonoBehaviour)
        {
            _activeMonoBehaviour = newActiveMonoBehaviour;
            _lastShootTime = 0;
            _trailPool = new ObjectPool<TrailRenderer>(CreateTrail);
           
            _model = Instantiate(modelPrefab, parent, false);
            _model.transform.localPosition = spawnPoint;
            _model.transform.localRotation = Quaternion.Euler(spawnRotation);

            _shootSystem = _model.GetComponentInChildren<ParticleSystem>();
            _audioSource = _model.GetComponentInChildren<AudioSource>();
        }
        private void Shoot()
        {
            if (Time.time - _lastShootTime - shootConfig.fireRate > Time.deltaTime)
            { 
                float lastDuration = Mathf.Clamp(0, (_shootingStopTime - _initialClickTime), shootConfig.maxSpreadTime);
                float lerpTime = (shootConfig.recoilRecoverySpeed - (Time.time - _shootingStopTime)) /
                                 shootConfig.recoilRecoverySpeed;

                _initialClickTime = Time.time - Mathf.Lerp(0, lastDuration, Mathf.Clamp01(lerpTime));
            }
            if (Time.time > shootConfig.fireRate + _lastShootTime)
            {
                _lastShootTime = Time.time;
                
                _shootSystem.Play();
                gunAudioConfig.PlayShootingClip(_audioSource);

                Vector3 spreadAmount = shootConfig.GetSpread(Time.time - _initialClickTime);
                _model.transform.forward += _model.transform.TransformDirection(spreadAmount);
                Vector3 shootDirection = _model.transform.forward;

                ammoConfig.currentClipAmmo--;
                
                if (Physics.Raycast(
                        _shootSystem.transform.position,
                        shootDirection,
                        out RaycastHit hit,
                        float.MaxValue,
                        shootConfig.hitMask))
                {
                    _activeMonoBehaviour.StartCoroutine(PlayTrail(_shootSystem.transform.position, hit.point, hit));
                }
                else
                {
                    _activeMonoBehaviour.StartCoroutine(PlayTrail(_shootSystem.transform.position,
                        _shootSystem.transform.position + (shootDirection * trailConfig.MissDistance), hit));
                }
            }
        }

        public bool CanReload()
        {
            return ammoConfig.CanReload();
        }

        public void EndReload()
        {
            ammoConfig.Reload();
        }
        public void TryToShoot(bool isFireBtnPressed)
        {
            _model.transform.localRotation = Quaternion.Lerp(_model.transform.localRotation,
                Quaternion.Euler(spawnRotation), Time.deltaTime * shootConfig.recoilRecoverySpeed);
            if (isFireBtnPressed)
            {
                _lastFrameFireBtnClicked = true;
                if(ammoConfig.currentClipAmmo > 0) Shoot();
                else gunAudioConfig.PlayEmptyClip(_audioSource);
            }
            else if (_lastFrameFireBtnClicked)
            {             
                _shootingStopTime = Time.time;
                _lastFrameFireBtnClicked = false;
            }
        }
        private TrailRenderer CreateTrail()
        {
            GameObject instance = new GameObject("Bullet Trail");
            TrailRenderer trail = instance.AddComponent<TrailRenderer>();
            trail.colorGradient = trailConfig.Color;
            trail.material = trailConfig.Material;
            trail.widthCurve = trailConfig.WidthCurve;
            trail.time = trailConfig.Duration;
            trail.minVertexDistance = trailConfig.MinVertexDistance;

            trail.emitting = false;
            trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

            return trail;
        }

     private IEnumerator PlayTrail(Vector3 startPoint, Vector3 endPoint, RaycastHit hit)
        {
            TrailRenderer instance = _trailPool.Get();
            instance.gameObject.SetActive(true);
            instance.transform.position = startPoint;
            yield return null; 

            instance.emitting = true;

            float distance = Vector3.Distance(startPoint, endPoint);
            float remainingDistance = distance;
            while (remainingDistance > 0)
            {
                instance.transform.position = Vector3.Lerp(
                    startPoint,
                    endPoint,
                    Mathf.Clamp01(1 - (remainingDistance / distance))
                );
                remainingDistance -= trailConfig.SimulationSpeed * Time.deltaTime;

                yield return null;
            }

            instance.transform.position = endPoint;

            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(damageConfig.GetDamage(distance));
                }
            }
            yield return new WaitForSeconds(trailConfig.Duration);
            yield return null;
            instance.emitting = false;
            instance.gameObject.SetActive(false);
            _trailPool.Release(instance);
        }

        public void StartReloading()
        {
            gunAudioConfig.PlayReloadClip(_audioSource);
        }
    }
}
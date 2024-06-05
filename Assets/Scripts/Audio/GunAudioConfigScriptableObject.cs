using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "Audio config", menuName = "Guns/Audio config", order = 7)]
    public class GunAudioConfigScriptableObject : ScriptableObject
    {
        [Range(0, 1f)]
        public float volume;
        public AudioClip fireClip;
        public AudioClip reloadClip;
        public AudioClip emptyClip;

        private readonly float _emptyClipCooldown = 0.5f;
        private float _lastEmptyClipPlayTime;

        public void PlayShootingClip(AudioSource audioSource)
        {
            if (fireClip != null)
                audioSource.PlayOneShot(fireClip, volume);
        }

        public void PlayReloadClip(AudioSource audioSource)
        {
            if (reloadClip != null)
                audioSource.PlayOneShot(reloadClip, volume);
        }

        public void PlayEmptyClip(AudioSource audioSource)
        {
            if (emptyClip != null && Time.time >= _lastEmptyClipPlayTime + _emptyClipCooldown)
            {
                audioSource.PlayOneShot(emptyClip, volume);
                _lastEmptyClipPlayTime = Time.time;
            }
        }
    }
}
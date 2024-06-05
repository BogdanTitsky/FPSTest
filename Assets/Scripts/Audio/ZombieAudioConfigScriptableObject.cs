using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "Audio config", menuName = "Enemy/Audio config", order = 7)]
    public class ZombieAudioConfigScriptableObject : ScriptableObject
    {
        [Range(0, 1f)]
        public float volume;
        public AudioClip biteClip;
        public AudioClip deathClip;


        public void PlayBiteClip(AudioSource audioSource)
        {
            if (biteClip != null)
                audioSource.PlayOneShot(biteClip, volume);
        }

        public void PlayDeathClip(AudioSource audioSource)
        {
            if (deathClip != null)
                audioSource.PlayOneShot(deathClip, volume);
        }

        
    }
}
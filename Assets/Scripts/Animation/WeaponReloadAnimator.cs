using System;
using Player;
using UnityEngine;

namespace Animation
{
    public class WeaponReloadAnimator : MonoBehaviour
    {
        [SerializeField] private PlayerAction playerAction;
        [SerializeField] private Animator animator;

        public event Action OnReloadComplete;
        private void OnEnable()
        {
            if (playerAction != null)
            {
                playerAction.OnReload += HandleReload;
            }
        }

        private void OnDisable()
        {
            if (playerAction != null)
            {
                playerAction.OnReload -= HandleReload;
            }
        }

        private void HandleReload()
        {
            animator.SetTrigger("Reload");
        }
        
        public void ReloadComplete()
        {
            OnReloadComplete?.Invoke();
        }
    }
}
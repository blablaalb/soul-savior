using System;
using UnityEngine;

namespace Characters.Wizards
{
    public class WizardAnimations : MonoBehaviour
    {
        private Animator _animator;

        public Action TakingSoulBegan;

        internal void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        public void Idle()
        {
            _animator.CrossFade("Idle01 0", 1f, 0, 0);
        }

        public void TakeSoul()
        {
            _animator.CrossFade("Attack02Start", 0.1f, 0, 0);
        }

        public void OnTakingSoulBegan()
        {
            TakingSoulBegan?.Invoke();
        }
    }
}
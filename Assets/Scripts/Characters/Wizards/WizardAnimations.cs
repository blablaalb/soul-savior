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

        public void Move()
        {
            _animator.CrossFade("WalkForward", 0.5f, 0, 0f);
        }

        public void Jump()
        {
            _animator.CrossFade("JumpStart", 0.5f, 0, 0f);
        }

        public void Land()
        {
            _animator.CrossFade("JumpEnd", 0.5f, 0, 0f);
        }

        public void OnTakingSoulBegan()
        {
            TakingSoulBegan?.Invoke();
        }


    }
}
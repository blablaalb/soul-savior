using UnityEngine;

namespace Characters.Pedestrians
{
    public class PedestrianAnimations : MonoBehaviour
    {
        private Animator _animator;
        private Animator Animator => _animator ??= GetComponentInChildren<Animator>(true);


        public void Walk()
        {
            Animator.CrossFade("Walk", normalizedTransitionDuration: 0.1f, layer: 0, normalizedTimeOffset: 0.1f);
        }

        public void Idle()
        {
            Animator.CrossFade("Idle", normalizedTransitionDuration: 0.1f, layer: 0, normalizedTimeOffset: 0.1f);
        }

        public void SoulBeingTaken()
        {
            Animator.CrossFade("being-possessed", normalizedTransitionDuration: 1f, layer: 0, normalizedTimeOffset: 0.1f);
        }

    }
}
using UnityEngine;
using Characters.Players;

namespace Characters.Pedestrians
{
    [System.Serializable]
    public class AttackState : IPedestrianState
    {
        private Player _player;
        private PedestrianAnimations _animations;
        private Rigidbody _rb;

        public string StateName => "Attack";

        public void Initialize(PedestrianAnimations animations, Player player, Rigidbody rb)
        {
            _animations = animations;
            _player = player;
            _rb = rb;
        }

        public void Enter()
        {
            _animations.Attack();
            // TODO: OnPLayerHit()
        }

        public void Exit()
        {
        }

        public void OnFixedUpdate()
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }

        public void OnUpdate()
        {
        }


    }

}
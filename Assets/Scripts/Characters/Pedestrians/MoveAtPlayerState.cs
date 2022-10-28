using UnityEngine;

namespace Characters.Pedestrians
{
    [System.Serializable]
    public class MoveAtPlayerState : IPedestrianState
    {
        private Player _player;
        private Transform _transform;
        [SerializeField]
        private float _moveSpeed;
        [SerializeField]
        private float _rotateSpeed;
        [SerializeField]
        private float _lookAngleThreshold;
        [SerializeField]
        private float _stopDistance;
        private PedestrianAnimations _animations;

        public string StateName => "Move at Player";

        public void Initialize(Transform transform, Player player, PedestrianAnimations animations)
        {
            _transform = transform;
            _player = player;
            _animations = animations;
        }

        public void Enter()
        {
            _animations.Walk();
        }

        public void Exit()
        {
        }

        public void OnFixedUpdate()
        {
        }

        public void OnUpdate()
        {
            if (Angle <= _lookAngleThreshold)
                if (Distance >= _stopDistance)
                    Move();
            Look();
        }

        private void Move()
        {
            var position = _transform.position;
            position += Direction * Time.deltaTime;
            _transform.position = position;
        }

        private void Look()
        {
            var targetRotation = Quaternion.LookRotation(Direction);
            var rotation = Quaternion.Lerp(_transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
            _transform.rotation = rotation;
        }

        private float Angle => Vector3.Angle(_transform.forward, Direction);
        private Vector3 Direction => _player.transform.position - _transform.position;
        private float Distance => Vector3.Distance(_player.transform.position, _transform.position);

    }
}
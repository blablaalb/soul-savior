using Common;
using UnityEngine;
using Characters.Players;

namespace Characters.Pedestrians
{
    [System.Serializable]
    public class MoveAtPlayerState : IPedestrianState
    {
        private Player _player;
        private Transform _transform;
        private float _moveSpeed;
        [SerializeField]
        private float _defaultMoveSpeed = 0.05f;
        [SerializeField]
        private float _rotateSpeed;
        [SerializeField]
        private float _lookAngleThreshold;
        [SerializeField]
        private float _stopDistance;
        private PedestrianAnimations _animations;
        private Vector3 _target;
        private Rigidbody _rigidBody;
        private Pedestrian _pedestrian;

        public string StateName => "Move at Player";
        public float MoveSpeed
        {
            get { return _moveSpeed; }
            set
            {
                _moveSpeed = value;
            }
        }
        public float RotateSpeed { get { return _rotateSpeed; } set { _rotateSpeed = value; } }

        public void Initialize(Transform transform, Player player, PedestrianAnimations animations)
        {
            _transform = transform;
            _player = player;
            _animations = animations;
            _rigidBody = _transform.GetComponent<Rigidbody>();
            _pedestrian = transform.GetComponent<Pedestrian>();
            _moveSpeed = _defaultMoveSpeed;
        }

        public void Enter()
        {
            _animations.Walk( _moveSpeed / _defaultMoveSpeed );
            _target = _player.transform.position;
        }

        public void Exit()
        {
            _rigidBody.velocity = Vector3.zero;
        }

        public void OnFixedUpdate()
        {
            if (Angle <= _lookAngleThreshold)
                if (Distance >= _stopDistance)
                    Move();
                else
                {
                    _rigidBody.velocity = Vector3.zero;
                    _pedestrian.Attack();
                }
        }

        public void OnUpdate()
        {
            Look();
        }

        private void Move()
        {
            var velocity = Direction * _moveSpeed;
            _rigidBody.velocity = velocity;
        }

        private void Look()
        {
            var targetRotation = Quaternion.LookRotation(Direction);
            var rotation = Quaternion.Lerp(_transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
            _transform.rotation = rotation;
        }

        private float Angle => Vector3.Angle(_transform.forward, Direction);
        private Vector3 Direction => _target - _transform.position;
        private float Distance => Vector3.Distance(_target, _transform.position);

    }
}
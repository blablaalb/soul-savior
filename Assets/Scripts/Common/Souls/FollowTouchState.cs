using UnityEngine;

namespace Ccmmon.Souls
{
    [System.Serializable]
    public class FollowTouchState : ISoulState
    {
        [SerializeField]
        private float _speed;
        private Transform _body;
        private Transform _transform;
        private Soul _soul;

        public Vector2 DirectionNormalized { get; set; }
        public string StateName => "Follow Touch";

        public void Initialize(Soul soul, Transform body)
        {
            _soul = soul;
            _transform = _soul.transform;
            _body = body;
        }


        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public void OnFixedUpdate()
        {
        }

        public void OnUpdate()
        {
            var xSpeed = DirectionNormalized.x * _speed * Time.deltaTime;
            var ySpeed = -(DirectionNormalized.y * _speed * Time.deltaTime);
            var zSpeed = -(DirectionNormalized.y * _speed * Time.deltaTime);

            var zyDirection = new Vector2(_body.position.z, _body.position.y) - new Vector2(_transform.position.z, _transform.position.y);
            zyDirection.Normalize();
            var newPosition = new Vector3(
                _transform.position.x + xSpeed,
                _transform.position.y + zyDirection.y * ySpeed,
                _transform.position.z + zyDirection.x * zSpeed
            );

            _transform.position = newPosition;
        }

    }
}
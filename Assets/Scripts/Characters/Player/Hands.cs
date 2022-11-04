using UnityEngine;

namespace Characters.Players
{
    public class Hands : MonoBehaviour
    {
        private int _interpolationFramesCount = 500;
        private int _elapsedFrames = 0;
        private float _z;
        [SerializeField]
        private float _yOffset;
        private Vector3 _defaultPositionLocal;
        private Vector3 _targetPosition;

        internal void Awake()
        {
            _z = transform.position.z - Camera.main.transform.position.z;
            _defaultPositionLocal = transform.localPosition;
        }

        internal void Start()
        {
            DefaultPosition();
        }

        internal void Update()
        {
            transform.position = Interpolate(_targetPosition);
        }

        public void FollowScreenPoint(Vector3 point)
        {
            point.z = _z;
            var position = Camera.main.ScreenToWorldPoint(point);
            position.y += _yOffset;
            _targetPosition = position;
        }

        public void DefaultPosition()
        {
            _targetPosition = transform.parent.TransformPoint(_defaultPositionLocal);
        }

        private Vector3 Interpolate(Vector3 position)
        {
            float interpolationRatio = (float)_elapsedFrames / _interpolationFramesCount;
            Vector3 interpolatedPosition = Vector3.Lerp(transform.position, position, interpolationRatio);
            _elapsedFrames = (_elapsedFrames + 1) % (_interpolationFramesCount + 1);
            return interpolatedPosition;
        }

    }
}
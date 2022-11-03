using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ommon.Souls
{
    public class RagdollController : MonoBehaviour
    {
        [SerializeField]
        private Transform _root;
        private Vector3 _lastPosition;
        private Vector3 CurrentPosition => _root.localPosition;
        private Vector3 _direction;
        private Vector3 _defaultOffset;
        private int _elapsedFrames = 0;
        private int _interpolationFramesCount;

        internal void Awake()
        {
            _defaultOffset = transform.localPosition;
            _interpolationFramesCount = 45;
        }

        internal void LateUpdate()
        {
            _direction = CurrentPosition - _lastPosition;
            _lastPosition = CurrentPosition;
        }

        internal void Update()
        {
            var position = _defaultOffset - _direction;
            Interpolate(position);
        }

        private void Interpolate(Vector3 position)
        {
            float interpolationRatio = (float)_elapsedFrames / _interpolationFramesCount;
            transform.localPosition = Vector3.Lerp(transform.localPosition, position, interpolationRatio);
            _elapsedFrames = (_elapsedFrames + 1) % (_interpolationFramesCount + 1);
        }

    }
}
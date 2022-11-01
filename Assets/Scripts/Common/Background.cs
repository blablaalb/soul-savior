using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Scenery
{
    public class Background : MonoBehaviour
    {
        private Camera _camera;
        private float _zOffset;

        internal void Awake()
        {
            _camera = Camera.main;
            _zOffset = Mathf.Abs(_camera.transform.position.z - transform.position.z);
        }


        internal void Update()
        {
            var position = _camera.transform.position;
            position.z += _zOffset;
            transform.position = position;
        }
    }
}
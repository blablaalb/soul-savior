using UnityEngine;

namespace Common
{
    public class Hands : MonoBehaviour
    {
        private float _z;
        [SerializeField]
        private float _yOffset;

        internal void Awake()
        {
            _z = transform.position.z - Camera.main.transform.position.z;
        }

        public void FollowScreenPoint(Vector3 point)
        {
            point.z = _z;
            var position = Camera.main.ScreenToWorldPoint(point);
            position.y += _yOffset;
            transform.position = position;
        }
    }
}
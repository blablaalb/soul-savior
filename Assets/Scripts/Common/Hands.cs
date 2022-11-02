using UnityEngine;

namespace Common
{
    public class Hands : MonoBehaviour
    {
        private float _z;

        internal void Awake()
        {
            _z = transform.position.z - Camera.main.transform.position.z;
        }

        public void FollowScreenPoint(Vector3 point)
        {
            point.z = _z;
            var position = Camera.main.ScreenToWorldPoint(point);
            transform.position = position;
        }
    }
}
using Characters.Players;
using Common.Souls;
using PER.Common;
using UnityEngine;

namespace Common.Inputs
{
    public class TouchInput : Singleton<TouchInput>
    {
        private Soul _soul;
        private Hands _hands;

        override protected void Awake()
        {
            base.Awake();
            _hands = FindObjectOfType<Hands>();
        }

        internal void Update()
        {
            if (Input.touches.Length > 0)
            {
                var touch = Input.touches[0];
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        OnTouchBegan(touch);
                        break;
                    case TouchPhase.Stationary:
                        break;
                    case TouchPhase.Moved:
                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        OnTouchEnded(touch);
                        break;
                }

                var delta = touch.deltaPosition;
                delta.x = Mathf.Clamp(delta.x, -1, 1);
                delta.y = Mathf.Clamp(delta.y, -1, 1);
                if (_soul != null)
                {
                    _soul.FollowTouch(delta);
                    _hands.FollowScreenPoint(touch.position);
                }
            }
        }

        private void OnTouchBegan(Touch touch)
        {
            var ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, float.MaxValue,  layerMask: LayerMask.GetMask("Ghost")))
            {
                if (hitInfo.collider.GetComponent<Soul>() is Soul soul)
                {
                    _soul = soul;
                }

            }
        }

        private void OnTouchEnded(Touch touch)
        {
            _soul?.OnDragEnded();
            _soul = null;
            _hands.DefaultPosition();
        }


    }
}
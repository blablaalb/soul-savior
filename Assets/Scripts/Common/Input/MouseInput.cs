using Ccmmon.Souls;
using PER.Common;
using UnityEngine;

namespace Common.Inputs
{
    public class MouseInput : Singleton<MouseInput>
    {
        private Soul _soul;
        private Vector2? _lastPointerPosition;

        internal void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.collider.GetComponent<Soul>() is Soul soul)
                    {
                        _soul = soul;
                        UpdateLastPointerPosition();
                    }
                }
            }
            else if (Input.GetMouseButton(0))
            {
                if (_soul != null)
                    _soul.FollowTouch(Delta());
                UpdateLastPointerPosition();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _lastPointerPosition = null;
                _soul?.ReturnToStack();
                _soul = null;
            }
        }

        private void UpdateLastPointerPosition()
        {
            _lastPointerPosition = Input.mousePosition;
        }

        private Vector2 Delta()
        {
            var delta = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - _lastPointerPosition.Value;
            delta.x = Mathf.Clamp(delta.x, -1, 1);
            delta.y = Mathf.Clamp(delta.y, -1, 1);
            return delta;
        }


    }

}
using System;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;
using Common;

public class Soul : MonoBehaviour
{
    [SerializeField]
    private float _moveTime = 1f;
    private Tween _moveTween;
    private Collider _collider;
    private Vector2? _lastTouchPos;
    private Vector2 TouchPosition => Input.mousePosition;
    [SerializeField]
    private float _dragSpeed;
    private Transform _body;
    [SerializeField]
    private float _maxTouchDistance;
    private StackMember _stackMember;

    public bool IsDraggedByPlayer => _lastTouchPos != null;

    internal void Awake()
    {
        _collider = GetComponent<Collider>();
        _body = GetComponentInParent<SoulAndBody>().Body.transform;
        _stackMember = GetComponent<StackMember>();
    }

    internal void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var touchPosition = TouchPosition;
            var ray = Camera.main.ScreenPointToRay(touchPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == _collider)
                {
                    _lastTouchPos = touchPosition;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _lastTouchPos = null;
            _stackMember.MoveToPlace(Ease.Linear);
        }

        if (_lastTouchPos != null)
        {
            var touchDirection = TouchPosition - _lastTouchPos.Value;

            touchDirection.x = Mathf.Clamp(touchDirection.x, -_maxTouchDistance, _maxTouchDistance);
            touchDirection.y = Mathf.Clamp(touchDirection.y, -_maxTouchDistance, _maxTouchDistance);

            touchDirection.x /= _maxTouchDistance;
            touchDirection.y /= _maxTouchDistance;

            var xSpeed = touchDirection.x * _dragSpeed * Time.deltaTime;
            var ySpeed = -touchDirection.y * _dragSpeed * Time.deltaTime;
            var zSpeed = -touchDirection.y * _dragSpeed * Time.deltaTime;

            var zyDirection = new Vector2(_body.transform.position.z, _body.transform.position.y) - new Vector2(transform.position.z, transform.position.y);
            zyDirection.Normalize();

            var newPosition = new Vector3(
                transform.position.x + xSpeed,
                transform.position.y + zyDirection.y * ySpeed,
                transform.position.z + zyDirection.x * zSpeed
                );
            
            transform.position = newPosition;

            // var newSoulPosition = new Vector3(transform.position.x + touchDirection.x * _dragSpeed * Time.deltaTime, transform.position.y, transform.position.z + touchDirection.y * _dragSpeed * Time.deltaTime);
        }
    }

    public async UniTask Move(Vector3 target)
    {
        if (_moveTween != null) _moveTween.Kill();
        _moveTween = transform.DOMove(target, _moveTime).OnComplete(() =>
        {
            _moveTween = null;
        });
        var task = _moveTween.ToUniTask();
        await task;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

}
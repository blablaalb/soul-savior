using System;
using UnityEngine;
using DG.Tweening;

public class Soul : MonoBehaviour
{
    [SerializeField]
    private float _time;
    private Tween _moveTween;

    public void Move(Vector3 target, Action callback = null)
    {
        if (_moveTween != null) _moveTween.Kill();
        _moveTween = transform.DOMove(target, _time).OnComplete(() =>
        {
            _moveTween = null;
            callback?.Invoke();
        });

    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }


}
using System;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;

public class Soul : MonoBehaviour
{
    [SerializeField]
    private float _moveTime = 1f;
    private Tween _moveTween;

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
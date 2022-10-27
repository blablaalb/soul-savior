using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class StackMember : MonoBehaviour
{
    public float Width;
    private Vector3 _stackPosition;
    private Tween _moveTween;
    [SerializeField]
    private float _moveTime;

    public Vector3 StackPosition
    {
        get { return _stackPosition; }
        set
        {
            _stackPosition = value;
        }
    }


    public async UniTask MoveToPlace(Ease ease = Ease.OutQuart)
    {
        if (_moveTween != null) _moveTween.Kill();
        _moveTween = transform.DOMove(StackPosition, _moveTime).SetEase(ease).OnComplete(() =>
        {
            _moveTween = null;
        });
        var task = _moveTween.ToUniTask();
        await task;
    }

    public void StopMoving()
    {
        _moveTween?.Kill();
    }


}
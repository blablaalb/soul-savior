using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;

public class StackMember : MonoBehaviour
{
    public float Width;
    private Vector3 _stackPosition;
    [SerializeField]
    private float _moveTime;
    private CancellationTokenSource _cts;

    public Vector3 StackPosition
    {
        get { return _stackPosition; }
        set
        {
            _stackPosition = value;
        }
    }


    public virtual async UniTask MoveToPlace(Ease ease = Ease.OutQuart)
    {
        _cts = new CancellationTokenSource();
        var moveTask = transform.DOMove(StackPosition, _moveTime).SetEase(ease).WithCancellation(_cts.Token);
        var rotateTask = transform.DORotate(HorizontalStack.Instance.transform.rotation.eulerAngles, _moveTime).WithCancellation(_cts.Token);
        await UniTask.WhenAll(moveTask, rotateTask);
    }

    public virtual async UniTask AddSelf(bool moveImmediately = false)
    {
        await HorizontalStack.Instance.Add(this, moveImmediately);
    }

    public virtual void StopMoving()
    {
        _cts.Cancel();
    }


    internal void OnDestroy()
    {
        _cts?.Cancel();
    }

}
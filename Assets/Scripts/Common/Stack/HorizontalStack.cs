using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using System;
using Cysharp.Threading.Tasks;

public class HorizontalStack : MonoBehaviour
{
    [SerializeField]
    private float _distance;
    private List<StackMember> _stackMembers = new List<StackMember>();
    private static HorizontalStack _instance;

    public static HorizontalStack Instance => _instance ??= _instance = FindObjectOfType<HorizontalStack>();


    public async UniTask Add(StackMember stackMember, bool moveImmediately = false)
    {
        _stackMembers.Add(stackMember);
        Sort(_stackMembers.Count - 1);
        stackMember.StackPosition = CalculatePosition(_stackMembers.Count - 1);
        if (moveImmediately)
            await stackMember.MoveToPlace();
    }

    private Vector3 CalculatePosition(int index)
    {
        var stackMember = _stackMembers[index];
        var totalWidth = (stackMember.Width + _distance) * _stackMembers.Count;
        var step = totalWidth / _stackMembers.Count;
        var pos = new Vector3(step * (index + .5f) - totalWidth / 2, 0, 0);
        pos = transform.TransformPoint(pos);
        return pos;
    }

    private void Sort(int count)
    {
#pragma warning disable CS4014
        for (int i = 0; i < count; i++)
        {
            var soul = _stackMembers[i];
            var position = CalculatePosition(i);
            soul.StackPosition = position;
            soul.MoveToPlace();
        }
#pragma warning restore CS4014
    }


}
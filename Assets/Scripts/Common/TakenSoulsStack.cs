using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using System;
using Cysharp.Threading.Tasks;

public class TakenSoulsStack : MonoBehaviour
{
    [SerializeField]
    private float _distance;
    private List<Soul> _souls = new List<Soul>();
    private static TakenSoulsStack _instance;

    public static TakenSoulsStack Instance => _instance ??= _instance = FindObjectOfType<TakenSoulsStack>();


    public Vector3 Add(Soul soul)
    {
        _souls.Add(soul);
        Sort(_souls.Count - 1);
        return CalculatePosition(_souls.Count - 1);
    }

    private Vector3 CalculatePosition(int index)
    {
        var totalWidth = _distance * _souls.Count;
        var step = totalWidth / _souls.Count;
        var pos = new Vector3(step * (index + .5f) - totalWidth / 2, 0, 0);
        Debug.Log(pos, _souls.Last());
        pos = transform.TransformPoint(pos);
        return pos;
    }

    private async void Sort(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var soul = _souls[i];
            var position = CalculatePosition(i);
            soul.Move(position);
        }
    }


}
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


    public void Add(Soul soul, Action callback = null)
    {
        if (_souls.Count == 0)
        {
            _souls.Add(soul);
        }

        _souls.Add(soul);
        Sort();
    }

    public void Sort()
    {
        var totalWidth = _distance * _souls.Count;
        var step = totalWidth / _souls.Count;
        var tasks = new List<UniTask>();
        for (var i = 0; i < _souls.Count; i++)
        {
            var soul = _souls[i];
            var pos = new Vector3(step * (i + .5f) - totalWidth / 2, 0, 0);
            pos = transform.TransformPoint(pos);
            soul.Move(pos);
        }
    }

}
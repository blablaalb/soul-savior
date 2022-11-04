using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _offset;

    internal void Awake()
    {
        _camera = Camera.main;
        _offset =  transform.position - _camera.transform.position;
    }


    internal void Update()
    {
        var position = _camera.transform.position +_offset;
        transform.position = position;
    }
}

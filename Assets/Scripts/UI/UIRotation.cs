using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotation : MonoBehaviour
{
    private Transform _camera;
    private void Start()
    {
        _camera = Camera.main.transform;
    }
    private void Update()
    {
        transform.LookAt(transform.position + _camera.rotation * Vector3.forward, _camera.rotation * Vector3.up);
    }
}

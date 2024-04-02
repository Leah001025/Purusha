using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUI : MonoBehaviour
{
    [SerializeField] private GameObject targetObj;
    private Transform battleCamera;
    private void Start()
    {
        battleCamera = Camera.main.transform;
    }
    private void Update()
    {
        transform.LookAt(transform.position + battleCamera.rotation * Vector3.forward, battleCamera.rotation * Vector3.up);
    }
    public void OnTarget()
    {
        targetObj.SetActive(true);
    }
    public void OffTarget()
    {
        targetObj.SetActive(false);
    }
}

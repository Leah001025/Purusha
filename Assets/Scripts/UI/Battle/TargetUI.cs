using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUI : MonoBehaviour
{
    [SerializeField] private GameObject targetObj;
    public void OnTarget()
    {
        targetObj.SetActive(true);
    }
    public void OffTarget()
    {
        targetObj.SetActive(false);
    }
}

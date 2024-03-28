using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUI : MonoBehaviour
{
    [SerializeField] private GameObject targetObj;

    private void Start()
    {
        BattleManager.Instance.OnTarget += OnTarget;
    }
    private void OnTarget(string targetName)
    {
        if (BattleManager.Instance != null)
        {
            if (targetName == gameObject.transform.parent.gameObject.transform.parent.gameObject.name)
            {
                targetObj.SetActive(true);
            }
            else
            {
                targetObj.SetActive(false);
            }
        }
    }
}

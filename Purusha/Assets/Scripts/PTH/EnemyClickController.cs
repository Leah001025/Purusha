using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClickController : MonoBehaviour
{
    private void Update()
    {
        if (BattleManager.Instance != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                BattleManager.Instance.target = gameObject;
            }
        }
    }
}

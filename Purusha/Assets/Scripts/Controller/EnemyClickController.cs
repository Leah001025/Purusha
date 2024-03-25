using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClickController : MonoBehaviour
{
    private void OnMouseOver()
    {
        if (BattleManager.Instance != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                BattleManager.Instance.target = gameObject;
                BattleManager.Instance.targerTrans = transform.parent;
            }
        }
    }
}

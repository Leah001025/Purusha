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
                if (BattleManager.Instance.isEnemyTurn) return;
                if (BattleManager.Instance.target != null)
                {
                    BattleManager.Instance.OffTarget();
                }
                BattleManager.Instance.target = gameObject.transform.parent.gameObject;
                BattleManager.Instance.OnTarget();
            }
        }
    }
}

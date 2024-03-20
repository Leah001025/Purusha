using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBattleUI : MonoBehaviour
{
    public void StartBattle()
    {
        BattleManager.Instance.BattleStart(110101);
    }
}

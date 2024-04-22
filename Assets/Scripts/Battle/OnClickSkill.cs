using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickSkill : MonoBehaviour 
{
    public void OnClickSkill1()
    {
        if(BattleManager.Instance.skillReady)
            BattleManager.Instance.CallSkill1Event();
    }
    public void OnClickSkill2()
    {
        if (BattleManager.Instance.skillReady)
            BattleManager.Instance.CallSkill2Event();
    }
    public void OnClickSkill3()
    {
        if (BattleManager.Instance.skillReady)
            BattleManager.Instance.CallSkill3Event();
    }
    public void OnClickSkill4()
    {
        if (BattleManager.Instance.skillReady)
            BattleManager.Instance.CallSkill4Event();
    }
}

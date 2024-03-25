using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickSkill : MonoBehaviour 
{
    public void OnClickSkill1()
    {
        BattleManager.Instance.CallSkill1Event();
    }
    public void OnClickSkill2()
    {
        BattleManager.Instance.CallSkill2Event();
    }
    public void OnClickSkill3()
    {
        BattleManager.Instance.CallSkill3Event();
    }
    public void OnClickSkill4()
    {
        BattleManager.Instance.CallSkill4Event();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickSkill : MonoBehaviour 
{
    public void OnClickSkill1()
    {
        TestBattle.Instance.CallSkill1Event();
    }
    public void OnClickSkill2()
    {
        TestBattle.Instance.CallSkill2Event();
    }
    public void OnClickSkill3()
    {
        TestBattle.Instance.CallSkill3Event();
    }
    public void OnClickSkill4()
    {
        TestBattle.Instance.CallSkill4Event();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UnitGauge
{
    private byte unitID; 
    private float unitGauge;
}
public class StageInfo
{

}
public class BattleManager : SingleTon<BattleManager>
{
    private List<UnitGauge> battleGauge;
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        CreateUnit();
    }
    private void Update()
    {
        //게이지 100인거 탐색
    }
    private void FixedUpdate()
    {
        //배틀 게이지 증가
    }
    private void CreateUnit()
    {
        //스테이지 정보 가져오기
        //플레이어 유닛 셋 가져오기
        var unitsObj = new GameObject("Units");
    }
}

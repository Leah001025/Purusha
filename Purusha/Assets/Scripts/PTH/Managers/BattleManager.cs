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
        //������ 100�ΰ� Ž��
    }
    private void FixedUpdate()
    {
        //��Ʋ ������ ����
    }
    private void CreateUnit()
    {
        //�������� ���� ��������
        //�÷��̾� ���� �� ��������
        var unitsObj = new GameObject("Units");
    }
}

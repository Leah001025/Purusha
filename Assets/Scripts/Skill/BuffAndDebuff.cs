using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuffAndDebuff : MonoBehaviour
{
    CharacterData characterBuffData;
    BuffDataBase buffData;
    public BuffData attackUp;
    public BuffData attackDown;
    public BuffData defUp;
    public BuffData defDown;
    public BuffData buffID;
    public BuffData shield;
    public float probability;
    public BuffAndDebuff(int id)
    {
        characterBuffData = (CharacterData)GameManager.Instance.User.characterDatas[id].CloneCharacter(id);
        buffData = (BuffDataBase)DataManager.Instance.BuffDB.Clone();
    }
    public void SetBuffandDebuff(int buffID)
    {
        switch (buffID)
        {
            case 101://쉴드
                SetShield(buffID);
                break;
            case 102://공증
                SetAtkUp(buffID);
                break;
            case 103://방증
                SetDefUp(buffID);
                break;
            case 201:
            case 202://공감
                SetAtkDown(buffID);
                break;
            case 203:
            case 204://방감
                SetDefDown(buffID);
                break;
            case 205:
            case 206://스턴
                SetShield(buffID);
                break;
            case 207:
            case 208:
            case 209://도발
                SetShield(buffID);
                break;
        }
    }
    public void SetShield(int buffId)
    {
        probability = Random.Range(0f, 1f);
        shield = buffData.GetData(buffId);
        if (probability <= buffData.GetData(buffId).DebuffProbability)
        {
            shield.CharacterData = characterBuffData.status.def * shield.Coefficient;
        }
    }
    public void SetAtkUp(int buffId)
    {
        probability = Random.Range(0f, 1f);
        attackUp = buffData.GetData(buffId);
        if (probability <= buffData.GetData(buffId).DebuffProbability)
        {
            attackUp.CharacterData = characterBuffData.status.atk * attackUp.Coefficient;
        }
    }
    public void SetDefUp(int buffId)
    {
        probability = Random.Range(0f, 1f);
        defUp = buffData.GetData(buffId);
        if (probability <= buffData.GetData(buffId).DebuffProbability)
        {
            defUp.CharacterData = characterBuffData.status.def * defUp.Coefficient;
        }
    }
    public void SetAtkDown(int buffId)
    {
        probability = Random.Range(0f, 1f);
        attackDown = buffData.GetData(buffId);
        if (probability <= buffData.GetData(buffId).DebuffProbability)
        {
            attackDown.CharacterData = characterBuffData.status.atk * attackDown.Coefficient;
        }
    }
    public void SetDefDown(int buffId)
    {
        probability = Random.Range(0f, 1f);
        defDown = buffData.GetData(buffId);
        if (probability <= buffData.GetData(buffId).DebuffProbability)
        {
            defDown.CharacterData = characterBuffData.status.def * defDown.Coefficient;
        }
    }
}

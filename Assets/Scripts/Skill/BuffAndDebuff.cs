using Structs;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuffAndDebuff
{
    CharacterData characterBuffData;
    EnemyCharacterData enemyCharacterBuffData;
    BuffDataBase buffData;
    public BuffData attackUp;
    public BuffData attackDown;
    public BuffData defUp;
    public BuffData defDown;
    public BuffData buffID;
    public BuffData shield;
    public BuffData stun;
    public BuffData provoke;
    public float probability;
    private string targetType;

    public BuffAndDebuff(int playerId = 0, int enemyId = 0, string type = "Player")
    {
        if (playerId != 0) characterBuffData = (CharacterData)GameManager.Instance.User.characters[playerId].CloneCharacter(playerId);
        else if (enemyId != 0) enemyCharacterBuffData = new EnemyCharacterData(enemyId);
        buffData = (BuffDataBase)DataManager.Instance.BuffDB.Clone();
        targetType = type;
    }
    //public void SetBuffandDebuff(int buffID)
    //{
    //    switch (buffID)
    //    {
    //        case 101://쉴드
    //            SetShield(buffID);
    //            break;
    //        case 102://공증
    //            SetAtkUp(buffID);
    //            break;
    //        case 103://방증
    //            SetDefUp(buffID);
    //            break;
    //        case 201:
    //        case 202://공감
    //            SetAtkDown(buffID);
    //            break;
    //        case 203:
    //        case 204://방감
    //            SetDefDown(buffID);
    //            break;
    //        case 205:
    //        case 206://스턴
    //            SetShield(buffID);
    //            break;
    //        case 207:
    //        case 208:
    //        case 209://도발
    //            SetShield(buffID);
    //            break;
    //    }
    //}
    public void SetShield(int buffId)
    {
        probability = Random.Range(0f, 1f);
        shield = buffData.GetData(buffId);
        if (probability <= buffData.GetData(buffId).DebuffProbability)
        {
            switch (targetType)
            {
                case "Player":
                    shield.CharacterData = characterBuffData.status.def * shield.Coefficient;
                    break;
                case "Enemy":
                    shield.CharacterData = enemyCharacterBuffData.enemyData.Def * shield.Coefficient;
                    break;
            }

        }
        else shield = null;
    }
    public void SetAtkUp(int buffId)
    {
        probability = Random.Range(0f, 1f);
        attackUp = buffData.GetData(buffId);
        if (probability <= buffData.GetData(buffId).DebuffProbability)
        {
            switch (targetType)
            {
                case "Player":
                    attackUp.CharacterData = characterBuffData.status.def * attackUp.Coefficient;
                    break;
                case "Enemy":
                    attackUp.CharacterData = enemyCharacterBuffData.enemyData.Atk * attackUp.Coefficient;
                    break;
            }
        }
        else attackUp = null;
    }
    public void SetDefUp(int buffId)
    {
        probability = Random.Range(0f, 1f);
        defUp = buffData.GetData(buffId);
        if (probability <= buffData.GetData(buffId).DebuffProbability)
        {
            switch (targetType)
            {
                case "Player":
                    defUp.CharacterData = characterBuffData.status.def * defUp.Coefficient;
                    break;
                case "Enemy":
                    defUp.CharacterData = enemyCharacterBuffData.enemyData.Def * defUp.Coefficient;
                    break;
            }
        }
        else defUp = null;
    }
    public void SetAtkDown(int buffId)
    {
        probability = Random.Range(0f, 1f);
        attackDown = buffData.GetData(buffId);
        if (probability <= buffData.GetData(buffId).DebuffProbability)
        {
            switch (targetType)
            {
                case "Player":
                    attackDown.CharacterData = characterBuffData.status.atk * attackDown.Coefficient;
                    break;
                case "Enemy":
                    attackDown.CharacterData = enemyCharacterBuffData.enemyData.Atk * attackDown.Coefficient;
                    break;
            }
        }
        else attackDown = null;
    }
    public void SetDefDown(int buffId)
    {
        probability = Random.Range(0f, 1f);
        defDown = buffData.GetData(buffId);
        if (probability <= buffData.GetData(buffId).DebuffProbability)
        {
            switch (targetType)
            {
                case "Player":
                    defDown.CharacterData = characterBuffData.status.def * defDown.Coefficient;
                    break;
                case "Enemy":
                    defDown.CharacterData = enemyCharacterBuffData.enemyData.Def * defDown.Coefficient;
                    break;
            }
        }
        else defDown = null;
    }
    public bool SetStun(int buffId)
    {
        probability = Random.Range(0f, 1f);
        stun = buffData.GetData(buffId);
        if (probability <= buffData.GetData(buffId).DebuffProbability)
        {
            return true;
        }
        else
        {
            stun = null;
            return false;
        }

    }
    public bool SetProvoke(int buffId)
    {
        probability = Random.Range(0f, 1f);
        provoke = buffData.GetData(buffId);
        if (probability <= buffData.GetData(buffId).DebuffProbability)
        {
            return true;
        }
        else
        {
            provoke = null;
            return false;
        }
    }
}

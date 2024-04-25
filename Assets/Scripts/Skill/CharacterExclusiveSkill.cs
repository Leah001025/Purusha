using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterExclusiveSkill
{
    private Dictionary<int, UnitInfo> lUnitInfo;
    private Dictionary<int, CharacterTurnController> characterSkillController;
    private Dictionary<int, EnemySkillController> enemySkillController;
    public void SkillUse(int charID)
    {
        switch(charID)
        {
            case 101:
                Breesha();
                break;
            case 102:
                Eve();
                break;
            case 103:
                Adam();
                break;
            case 104:
                Abel();
                break;
            case 105:
                Kain();
                break;
            case 106:
                Kana();
                break;
        }
    }

    private void Breesha()
    {
        characterSkillController = BattleManager.Instance.turnControllers;
        foreach (CharacterTurnController turnController in characterSkillController.Values)
        {
            turnController.SetBuffandDebuff(102);
            if (102 != 0) turnController.SetBuffandDebuff(102);
        }
    }
    private void Eve()
    {
        enemySkillController = BattleManager.Instance.enemySkillControllers;
        foreach (EnemySkillController skillController in enemySkillController.Values)
        {
            skillController.SetBuffandDebuff(202);
            if (202 != 0) skillController.SetBuffandDebuff(202);
        }
    }
    private void Adam()
    {
        enemySkillController = BattleManager.Instance.enemySkillControllers;
        foreach (EnemySkillController skillController in enemySkillController.Values)
        {
            skillController.SetBuffandDebuff(204);
            if (204 != 0) skillController.SetBuffandDebuff(204);
        }
    }
    private void Abel()
    {
        lUnitInfo = BattleManager.Instance.lUnitInfo;
        foreach (UnitInfo _unitData in lUnitInfo.Values)
        {
            if (_unitData.unitType == CharacterType.Player)
            {
                _unitData.unitGauge += 20;
            }
        }
    }
    private void Kain()
    {
        characterSkillController = BattleManager.Instance.turnControllers;
        foreach (CharacterTurnController turnController in characterSkillController.Values)
        {
            turnController.SetBuffandDebuff(101);
            if (101 != 0) turnController.SetBuffandDebuff(101);
        }
    }
    private void Kana()
    {
        characterSkillController = BattleManager.Instance.turnControllers;
        foreach (CharacterTurnController turnController in characterSkillController.Values)
        {
            turnController.SetBuffandDebuff(103);
            if (103 != 0) turnController.SetBuffandDebuff(103);
        }
    }
}

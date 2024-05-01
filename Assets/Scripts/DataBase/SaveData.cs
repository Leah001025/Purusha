using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[System.Serializable]
public class SaveData
{
    public List<UpgradeStatData> upgrades = new List<UpgradeStatData>();
    public List<CharacterStatus> teamStatusData = new List<CharacterStatus>();
    public List<CharacterSkillList> teamSkillData = new List<CharacterSkillList>();
    public List<CharacterStatus> statusData = new List<CharacterStatus>();
    public List<CharacterSkillList> skillData = new List<CharacterSkillList>();
    public List<Item> itemInventory = new List<Item>();
    public List<StageInfo> stageClear = new List<StageInfo>();


    public void SetData()
    {
        Dictionary<int, UpgradeStatData> upgradeDatas = GameManager.Instance.User.upgrades;
        Dictionary<int, CharacterData> characterData = GameManager.Instance.User.characters;
        Dictionary<int, CharacterData> teamData = GameManager.Instance.User.teamData;
        Dictionary<int, Item> items = GameManager.Instance.User.itemInventory;
        Stack<StageInfo> stageInfos = GameManager.Instance.User.stageClear;
        ReSetSaveData();
        foreach (KeyValuePair<int,UpgradeStatData> upgradeData in upgradeDatas)
        {
            upgradeData.Value.id = upgradeData.Key;
            upgrades.Add(upgradeData.Value);
        }
        foreach (Item item in items.Values)
        {
            itemInventory.Add(item);
        }
        if(stageInfos !=  null)
        {
            foreach (StageInfo stage in stageInfos)
            {
                stageClear.Add(stage);
            }
        }
        foreach (CharacterData character in characterData.Values)
        {
            statusData.Add(character.status);
            CharacterSkillList skillList = new CharacterSkillList();
            skillList.id = character.status.iD;
            foreach (CharacterSkill skill in character.skillData.Values)
            {
                skillList.skillList.Add(skill);
            }
            skillData.Add(skillList);
        }
        foreach (KeyValuePair<int,CharacterData> team in teamData)
        {
            team.Value.status.teamNum = team.Key;
            teamStatusData.Add(team.Value.status);
            CharacterSkillList skillList = new CharacterSkillList();
            skillList.id = team.Value.status.iD;
            foreach (CharacterSkill skill in team.Value.skillData.Values)
            {
                skillList.skillList.Add(skill);
            }
            teamSkillData.Add(skillList);
        }
    }
    public void GetData()
    {
        ReSetUserData();
        foreach (UpgradeStatData upgradeData in upgrades)
        {
            GameManager.Instance.User.upgrades.Add(upgradeData.id, upgradeData);
        }
        foreach (Item item in itemInventory)
        {
            GameManager.Instance.User.itemInventory.Add(item.id, item);
        }
        if (stageClear.Count != 0)
        {
            for (int i = stageClear.Count - 1; i >= 0; i--)
            {
                GameManager.Instance.User.stageClear.Push(stageClear[i]);
            }
            //foreach (StageInfo stage in stageClear)
            //{
            //    GameManager.Instance.User.stageClear.Push(stage);
            //}
        }
        foreach (CharacterStatus character in statusData)
        {
            CharacterData characterTemp = new CharacterData(character.iD);
            characterTemp.status = character;
            characterTemp.skillData.Clear();
            foreach (CharacterSkillList skill in skillData)
            {
                if(characterTemp.status.iD == skill.id)
                {
                    for (int i = 0;i<4;i++)
                    {
                        characterTemp.skillData.Add(i + 1, skill.skillList[i]);
                    }
                }
            }
            GameManager.Instance.User.characters.Add(characterTemp.status.iD,characterTemp);
        }
        foreach (CharacterStatus team in teamStatusData)
        {
            CharacterData characterTemp = new CharacterData(team.iD);
            characterTemp.status = team;
            characterTemp.skillData.Clear();
            foreach (CharacterSkillList skill in teamSkillData)
            {
                if (characterTemp.status.iD == skill.id)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        characterTemp.skillData.Add(i + 1, skill.skillList[i]);
                    }
                }
            }
            GameManager.Instance.User.teamData.Add(characterTemp.status.teamNum, characterTemp);
        }
    }
    private void ReSetSaveData()
    {
        upgrades.Clear();
        itemInventory.Clear();
        stageClear.Clear();
        statusData.Clear();
        skillData.Clear();
        teamStatusData.Clear();
        teamSkillData.Clear();
    }
    private void ReSetUserData()
    {
        GameManager.Instance.User.upgrades = new Dictionary<int, UpgradeStatData>();
        GameManager.Instance.User.characters = new Dictionary<int, CharacterData>();
        GameManager.Instance.User.teamData = new Dictionary<int, CharacterData>();
        GameManager.Instance.User.itemInventory = new Dictionary<int, Item>();
        GameManager.Instance.User.stageClear = new Stack<StageInfo>();
        //GameManager.Instance.User.isCutScenePlay = new Dictionary<int, bool>();
        if (stageClear.Count == 0) 
        {
            StageInfo stageInfo = new StageInfo();
            stageInfo.stageID = 1101;
            stageClear.Add(stageInfo);      
        }
    }
}


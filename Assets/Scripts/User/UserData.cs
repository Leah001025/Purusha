using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public int userLv;
    public int userExp;
    public string userName;
    public int gold;
    public int cash;
    public bool introScene;
    public bool tutorial1;
    public bool tutorial2;
    public bool isCutScenePlay = false;
    public SaveData saveData;
    public Stack<StageInfo> stageClear = new Stack<StageInfo>();
    public Dictionary<int, UpgradeStatData> upgrades = new Dictionary<int, UpgradeStatData>();
    public Dictionary<int, CharacterData> characters = new();
    public Dictionary<int, CharacterData> teamData = new Dictionary<int, CharacterData>(5);
    public Dictionary<int, Item> itemInventory = new Dictionary<int, Item>();
    //public Dictionary<int, bool> isCutScenePlay = new Dictionary<int, bool>();
    //public List<ItemData> Inventory = new();
    //public ItemDataBase ItemData = DataManager.Instance.ItemDB; 아이템데이터 바로 사용금지

    //UseData 초기 생성
    public UserData(string name)
    {
        userLv = 1;
        userExp = 0;
        userName = name;
        gold = 1000;
        cash = 100;
        saveData = new SaveData();

        //AddCharacter(101);
        AddCharacter(102);
        //AddCharacter(103);
        //AddCharacter(104);
        //AddCharacter(105);

        //AddTeam(101);
        AddTeam(102);
        //AddTeam(103);
        //AddTeam(104);
        //AddTeam(105);
        AddItem(10201, 10);
        AddItem(10301, 10);
        AddItem(10501, 10);


        //upgrades.Add(eve.status.iD, new UpgradeStatData());

        StageInfo stageInfo = new StageInfo();
        stageInfo.stageID = 1101;
        stageClear.Push(stageInfo);
    }
    //캐릭터 추가
    public void AddCharacter(int id)
    {
        CharacterData newCharacter = new CharacterData(id);
        UpgradeStatData newUpgradeStat = new UpgradeStatData();
        characters.Add(newCharacter.status.iD, newCharacter);
        upgrades.Add(newCharacter.status.iD, newUpgradeStat);
    }

    //팀원 추가
    public void AddTeam(int id)
    {
        for (int i = 1; i <= 5; i++)
        {
            if (!teamData.ContainsKey(i))
            {
                teamData.Add(i, characters[id]);
                Debug.Log(teamData[i]);
                return;
            }
        }
    }

    //팀원 제거
    public void RemoveTeam(int index)
    {
        teamData.Remove(index);
    }

    public void AddItem(int id, int quantity = 1)
    {
        Item newItem = new Item(id);
        if (!itemInventory.ContainsKey(id))
        {
            itemInventory.Add(id, newItem);
            itemInventory[id].quantity += (quantity-1);
        }
        else
        {
            itemInventory[id].quantity += quantity;
        }
    }
    public void RemoveItem(int id)
    {
        Item newItem = new Item(id);
        if (itemInventory.ContainsKey(id))
        {
            itemInventory[id].quantity--;
            if (itemInventory[id].quantity <= 0) itemInventory.Remove(id);
        }
        else
        {
            Debug.Log("인벤토리에 삭제할 아이템이 없습니다.");
        }
    }
    public int NextStage()
    {
        int curID = stageClear.Peek().stageID;
        int nextID;
        string idstr = curID.ToString();
        if (idstr.Substring(3) == "3")
        {
            nextID = curID + 100 - 2;
            return nextID;
        }
        else
        {
            nextID = curID + 1;
            return nextID;
        }
    }
    public void UpdateTeamData()
    {
        for (int i = 1; i <= teamData.Count; i++) 
        {
            teamData[i] = characters[teamData[i].status.iD];
        }
    }
    public void UpdateCharacterData()
    {
        for (int i = 1; i <= teamData.Count; i++)
        {
            characters[teamData[i].status.iD] = teamData[i];
        }

    }
    public void ResetCharacterHP()
    {
        foreach (CharacterData characterData in characters.Values)
        {
            characterData.status.health = characterData.status.maxhealth;
        }
        foreach (CharacterData characterData in teamData.Values)
        {
            characterData.status.health = characterData.status.maxhealth;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public int userLv;
    public int userExp;
    public string userName;
    public int gold;
    public int cash;
    public Stack<StageInfo> stageClear;
    public Dictionary<int, CharacterData> characterDatas = new();
    public Dictionary<int, CharacterData> teamData = new Dictionary<int, CharacterData>(5);
    public List<ItemData> Inventory = new();

    public ItemDataBase ItemData = DataManager.Instance.ItemDB;

    //UseData 초기 생성
    public UserData(string name)
    {
        userLv = 1;
        userExp = 0;
        userName = name;
        gold = 1000;
        cash = 100;
        CharacterData eve = new CharacterData(102);
        characterDatas.Add(eve.status.iD, eve);
        AddTeam(eve.status.iD);
        // stageClear.Push(110101);
    }
    //캐릭터 추가
    public void AddCharacter(int id)
    {
        CharacterData newCharacter = new CharacterData(id);
        characterDatas.Add(newCharacter.status.iD, newCharacter);
    }

    //팀원 추가
    public void AddTeam(int id)
    {
        for (int i = 1;  i <=5; i++)
        {
            if (!teamData.ContainsKey(i)) 
            {
                teamData.Add(i, characterDatas[id]);
                return;
            }
        }        
    }

    //팀원 제거
    public void RemoveTeam(int index)
    {
        teamData.Remove(index);
    }

    public void AddItem(int id)
    {
        foreach (ItemData _itemData in Inventory)
        {
            if (_itemData.ID == id)
            {
                _itemData.Quantity++;
            }
            else
            {
                Inventory.Add(ItemData.GetData(id));
            }
        }
    }
}
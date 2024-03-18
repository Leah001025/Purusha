using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public int userLv;
    public int userExp;
    public Dictionary<int, CharacterData> characterDatas = new();
    public Dictionary<int, CharacterData> teamData = new Dictionary<int, CharacterData>(5);

    //UseData �ʱ� ����
    public UserData()
    {
        userLv = 1;
        userExp = 0;
        CharacterData eve = new CharacterData(101);
        characterDatas.Add(eve.status.ID, eve);   
    }
    //ĳ���� �߰�
    public void AddCharacter(int id)
    {
        CharacterData newCharacter = new CharacterData(id);
        characterDatas.Add(newCharacter.status.ID, newCharacter);
    }

    //���� �߰�
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

    //���� ����
    public void RemoveTeam(int index)
    {
        teamData.Remove(index);
    }


}



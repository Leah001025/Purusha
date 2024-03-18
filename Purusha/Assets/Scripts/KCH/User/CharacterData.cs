using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData 
{
    public PlayerData status;
    public Dictionary<int, SkillData> skillData = new Dictionary<int, SkillData>(4);

    public CharacterData(int id)
    {
        status = DataManager.Instance.PlayerDB.GetData(id);
        for (int i = 1; i<=4; i++)
        {
            string skillIndex = id+$"{i:D2}".ToString()+"1";
            skillData.Add(i, DataManager.Instance.SkillDB.GetData(int.Parse(skillIndex)));
        }
    }
    
}

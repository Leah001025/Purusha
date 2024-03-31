using System.Collections.Generic;
using UnityEngine;

public class TeamFormation : MonoBehaviour
{
    public GameObject CharacterList;
    public Dictionary<string, string> characterPrefabMap = new Dictionary<string, string>();

    void Start()
    {                   // 캐릭터이름, 프리팹이름
        characterPrefabMap.Add("Eve", "Eve");
        characterPrefabMap.Add("Breesha", "Breesha");
        characterPrefabMap.Add("Kana", "Kana");
        characterPrefabMap.Add("Lain", "Lain");
        characterPrefabMap.Add("Adam", "Adam");
        characterPrefabMap.Add("Abel", "Abel");
        //ClickedCharacter(1);
    }

    public void ClickedCharacter(int characterID)
    {
        if (IsCharacterInTeam(characterID))
        {
            RemoveCharacterFromTeam(characterID);
        }
        else
        {
            AddCharacterToTeam(characterID);
        }
    }

    private bool IsCharacterInTeam(int characterID)
    {
        Dictionary<int, CharacterData> teamData = GameManager.Instance.User.teamData;
        return teamData.ContainsKey(characterID);
    }

    private void AddCharacterToTeam(int characterID)
    {
        CharacterData characterData = GameManager.Instance.User.teamData[characterID];
        string characterPrefabName = GetCharacterPrefabName(characterData.status.name);

        GameManager.Instance.AddTeam(characterID);
        CreateCharacterPrefab(characterPrefabName);
    }

    private void RemoveCharacterFromTeam(int characterID)
    {
        GameManager.Instance.RemoveTeam(characterID);
    }

    private string GetCharacterPrefabName(string characterName)
    {
        if (characterPrefabMap.ContainsKey(characterName))
        {
            return characterPrefabMap[characterName];
        }
        else
        {
            return null;
        }
    }

    public void CreateCharacterPrefab(string characterPrefabName)
    {
        GameObject characterPrefab = Resources.Load<GameObject>("Prefabs/Inventory/" + characterPrefabName);

        if (characterPrefab != null && CharacterList != null)
        {
            GameObject characterInstance = Instantiate(characterPrefab, CharacterList.transform);
            Debug.Log(characterInstance);
        }
    }

    public void UpdateCharacterListDisplay()
    {
        foreach (Transform child in CharacterList.transform)
        {
            //뭐더라
        }
        Dictionary<int, CharacterData> teamData = GameManager.Instance.User.teamData;

        foreach (var characterID in teamData.Keys)
        {
            CharacterData characterData = GameManager.Instance.User.teamData[characterID];
            string characterPrefabName = GetCharacterPrefabName(characterData.status.name);

            CreateCharacterPrefab(characterPrefabName);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TeamFormation : MonoBehaviour
{
    public GameObject CharacterList;
    public GameObject Team;
    public Dictionary<string, string> characterPrefabMap = new Dictionary<string, string>();

    void Start()
    {                   // 캐릭터이름, 프리팹이름
        characterPrefabMap.Add("이브", "Eve");
        characterPrefabMap.Add("브리샤", "Breesha");
        characterPrefabMap.Add("카나", "Kana");
        characterPrefabMap.Add("라인", "Lain");
        characterPrefabMap.Add("아담", "Adam");
        characterPrefabMap.Add("아벨", "Abel");
        //ClickedCharacter(1);
        init();
    }

    private void init()
    {
        foreach (var pair in GameManager.Instance.User.characterDatas.Values)
        {
            string prefabPath = "Prefabs/Inventory/" + pair.status.iD.ToString();
            var _res = Resources.Load(prefabPath) as GameObject;
            var _obj = Instantiate(_res, CharacterList.transform);
            _obj.name = pair.status.iD.ToString();
        }
        CurrentTeamSlot();
    }

    public void CurrentTeamSlot()
    {
        if (GameManager.Instance.User.teamData.Count > 0)
        {
            for(int key = 1; GameManager.Instance.User.teamData.Count >= key; key++)
            {
                string prefabPath = "Prefabs/TeamFormation/" + GameManager.Instance.User.teamData[key].status.iD;
                var _res = Resources.Load(prefabPath) as GameObject;
                var _obj = Instantiate(_res, Team.transform);
                _obj.name = GameManager.Instance.User.teamData[key].status.iD.ToString();
            }
        }
    }

    private bool AddCharacterToTeam(int characterID)
    {
        bool inTeam = false;
        foreach (CharacterData _data in GameManager.Instance.User.teamData.Values)
        {
            if (_data.status.iD == characterID)
            {
                inTeam = true;
            }
        }
        if (inTeam == false)
        {
            GameManager.Instance.AddTeam(characterID);
            return true;
        }
        return false;
    }

    public void OnPointerClick(string characterID)
    {
        if (Team.transform.childCount < 5 && AddCharacterToTeam(int.Parse(characterID)))
        {
            string prefabPath = "Prefabs/TeamFormation/" + characterID;
            var _res = Resources.Load(prefabPath) as GameObject;
            var _obj = Instantiate(_res, Team.transform);
            _obj.name = characterID;
        }
        Debug.Log(GameManager.Instance.User.teamData.Count);
    }

    public void OffPointerClick(string characterID)
    {
        int index = 0;
        foreach (KeyValuePair<int, CharacterData> _data in GameManager.Instance.User.teamData)
        {
            if (_data.Value.status.iD == int.Parse(characterID))
            {
                index = _data.Key;
            }
        }
        GameManager.Instance.RemoveTeam(index);
        Debug.Log(GameManager.Instance.User.teamData.Count);
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
}

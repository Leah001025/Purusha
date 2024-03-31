using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class TeamFormation : MonoBehaviour
{
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

    private void AddCharacterToTeam(int characterID) // 추가
    {
        GameManager.Instance.AddTeam(characterID);
    }

    private void RemoveCharacterFromTeam(int characterID) // 제거
    {
        GameManager.Instance.RemoveTeam(characterID);
    }
}

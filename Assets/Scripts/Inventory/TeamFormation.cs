using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;

public class TeamFormation : MonoBehaviour
{
    public List<Character> teamMembers = new List<Character>(); // 팀 멤버를 저장할 리스트

    // 캐릭터를 팀에 추가하는 함수
    public void AddCharacterToTeam(Character character)
    {
        if (!teamMembers.Contains(character)) // 팀에 이미 포함되어 있지 않은 경우에만 추가
        {
            teamMembers.Add(character);
            Debug.Log(character.name + "이(가) 팀에 추가되었습니다.");
            // 여기서 팀에 캐릭터를 추가하는 시각적인 업데이트를 해줄 수 있습니다. (예: UI 업데이트)
        }
    }

    // 팀에서 캐릭터를 제거하는 함수
    public void RemoveCharacterFromTeam(Character character)
    {
        if (teamMembers.Contains(character)) // 팀에 포함되어 있는 경우에만 제거
        {
            teamMembers.Remove(character);
            Debug.Log(character.name + "이(가) 팀에서 제거되었습니다.");
            // 여기서 팀에서 캐릭터를 제거하는 시각적인 업데이트를 해줄 수 있습니다. (예: UI 업데이트)
        }
    }
}
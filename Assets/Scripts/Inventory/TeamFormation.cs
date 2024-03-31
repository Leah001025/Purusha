using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;

public class TeamFormation : MonoBehaviour
{
    public List<Character> teamMembers = new List<Character>(); // �� ����� ������ ����Ʈ

    // ĳ���͸� ���� �߰��ϴ� �Լ�
    public void AddCharacterToTeam(Character character)
    {
        if (!teamMembers.Contains(character)) // ���� �̹� ���ԵǾ� ���� ���� ��쿡�� �߰�
        {
            teamMembers.Add(character);
            Debug.Log(character.name + "��(��) ���� �߰��Ǿ����ϴ�.");
            // ���⼭ ���� ĳ���͸� �߰��ϴ� �ð����� ������Ʈ�� ���� �� �ֽ��ϴ�. (��: UI ������Ʈ)
        }
    }

    // ������ ĳ���͸� �����ϴ� �Լ�
    public void RemoveCharacterFromTeam(Character character)
    {
        if (teamMembers.Contains(character)) // ���� ���ԵǾ� �ִ� ��쿡�� ����
        {
            teamMembers.Remove(character);
            Debug.Log(character.name + "��(��) ������ ���ŵǾ����ϴ�.");
            // ���⼭ ������ ĳ���͸� �����ϴ� �ð����� ������Ʈ�� ���� �� �ֽ��ϴ�. (��: UI ������Ʈ)
        }
    }
}
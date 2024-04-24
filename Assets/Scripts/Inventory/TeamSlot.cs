using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TeamSlot : MonoBehaviour
{
    private GameObject EditTeam;
    private TeamFormation teamFormation;

    void Start()
    {
        EditTeam = transform.parent.transform.parent.gameObject;
        teamFormation = EditTeam.GetComponent<TeamFormation>();
    }

    public void RemoveTeamSlot()
    {
        SoundManager.Instance.ButtonAudio("CharacterDown");
        teamFormation.OffPointerClick(gameObject.name);
        Destroy(gameObject);
    }
}

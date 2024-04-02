using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSlot : MonoBehaviour
{
    private GameObject EditTeam;
    private TeamFormation teamFormation;

    void Start()
    {
        EditTeam = transform.parent.transform.parent.gameObject;
        teamFormation = EditTeam.GetComponent<TeamFormation>();
    }

    public void AddTeamSlot()
    {
        teamFormation.OnPointerClick(gameObject.name);
    }
}

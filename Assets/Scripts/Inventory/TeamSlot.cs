using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        teamFormation.OffPointerClick(gameObject.name);
        Destroy(gameObject);
    }
}

using System;
using UnityEngine;

public class GameManager : SingleTon<GameManager>
{
    public UserData User;
    protected override void Awake()
    {
        base.Awake();
        User = new UserData();
    }
    private void Start()
    {
        User.AddCharacter(102);
        User.AddTeam(101);
        User.AddTeam(102);
        User.teamData[1].status.Health -= 50;
        User.teamData[2].status.Health -= 500;
        Debug.Log("팀데이터1: " + User.teamData[1].status.Health + User.teamData[1].status.Name);
        Debug.Log("팀데이터2: " + User.teamData[2].status.Health + User.teamData[2].status.Name);
        Debug.Log("캐릭터1: " + User.characterDatas[101].status.Health);
        Debug.Log("캐릭터2: " + User.characterDatas[102].status.Health);
    }


}

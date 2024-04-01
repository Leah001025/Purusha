using System;
using UnityEngine;

public class GameManager : SingleTon<GameManager>
{
    public int stageID = 1101;
    public int waveID;

    public bool wave1Clear = false;
    public bool wave2Clear = false;
    public bool wave3Clear = false;

    public UserData User;
    public string userName = "Leah";
    protected override void Awake()
    {
        base.Awake();
        User = new UserData(userName);
        User.AddCharacter(101);
        User.AddCharacter(103);
        User.AddCharacter(104);
        User.AddCharacter(105);
        User.characterDatas[101].status.speed = 1.05f;
        User.characterDatas[102].status.speed = 1.15f;
        User.characterDatas[103].status.speed = 1.3f;
        User.characterDatas[104].status.speed = 1.45f;
    }
    private void Start()
    {

    }

    public void AddItem(int id)
    {
        User.AddItem(id);
    }

    public void AddTeam(int id)
    {
        User.AddTeam(id);
    }

    public void RemoveTeam(int id)
    {
        User.RemoveTeam(id);
    }
}

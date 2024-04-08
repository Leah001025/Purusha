using System;
using UnityEngine;

public class StageInfo
{
    public int stageID;
    public bool wave1Clear;
    public bool wave2Clear;
    public bool wave3Clear;
}
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
        User.AddItem(10101);
        User.AddItem(10102);
        User.AddItem(10103);
        User.AddItem(10201);
        User.AddItem(10301);
        User.AddItem(10401);
        User.AddItem(10402);
        User.AddItem(10501);
        User.itemInventory[10501].quantity = 1000;
        User.itemInventory[10102].quantity = 100;
        User.itemInventory[10103].quantity = 50;
        User.itemInventory[10201].quantity = 100;
        User.itemInventory[10301].quantity = 100;
        User.itemInventory[10402].quantity = 10;
        User.characterDatas[102].status.health = 100;
    }
    private void Start()
    {

    }

    public void AddItem(int id)
    {
        User.AddItem(id);
    }
    public void RemoveItem(int id)
    {
        User.RemoveItem(id);
    }

    public void AddTeam(int id)
    {
        User.AddTeam(id);
    }

    public void RemoveTeam(int id)
    {
        User.RemoveTeam(id);
    }

    public void StageSelect(int stageNumber) // 챕터 들어간 후 스테이지 누르는 버튼에 연결
    {
        stageID = stageNumber;
    }
}

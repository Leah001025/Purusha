using System;
using UnityEngine;

[System.Serializable]
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
    }
    private void Start()
    {
        User = new UserData(userName);
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
    [ContextMenu("To Json Data")]
    public void SaveDatas()
    {
        User.saveData.SetData();
        Utility.SaveToJsonFile(User, "UserData.json");
        Debug.Log("데이터 저장했음");
    }
    [ContextMenu("From Json Data")]
    public void LoadDatas()
    {
        if (!Utility.IsExistsFile("UserData.json")) return;
        User = Utility.LoadJsonFile<UserData>("UserData.json");
        User.saveData.GetData();
        Debug.Log("데이터 불러왔음");
    }
}

using System;
using UnityEngine;

public class GameManager : SingleTon<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
        if (Instance != null) Init();
    }

    private void Init()
    {
        DataManager = ScriptableObject.CreateInstance<DataManager>();
    }

    public static DataManager DataManager { get; private set; }

}

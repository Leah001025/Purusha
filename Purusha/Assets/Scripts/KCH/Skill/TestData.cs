using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestData : DataBase<TestData>
{
    public int ID;
    public string Name;
    public string Description;
    public int Type;

    public override void SetData(TestData testData)
    {
        this.ID = testData.ID;
        this.Name = testData.Name;
        this.Description = testData.Description;
        this.Type = testData.Type;

    }
}

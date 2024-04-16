using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System.IO;

public static class Utility
{
    private readonly static string _dataPath = Application.persistentDataPath + "/Data/";

    public static int GetHashWithString(string path)
    {
        return path.GetHashCode();
    }
    public static int GetHashWithTag(GameObject path)
    {
        return path.tag.GetHashCode();
    }
    public static int GetLayerWithTag(GameObject path)
    {
        return path.layer.GetHashCode();
    }
    public static void SaveToJsonFile<T>(T data, string path)
    {
        File.WriteAllText(_dataPath + path, JsonUtility.ToJson(data));
        Debug.Log($"Save file : {_dataPath + path}");
    }

    public static T LoadJsonFile<T>(string path)
    {
        Assert.IsTrue(File.Exists(_dataPath + path), $"Exists Json File is Null : {_dataPath}{path}");

        string sr = File.ReadAllText(_dataPath + path);
        return JsonUtility.FromJson<T>(sr);
    }
    public static bool IsExistsFile(string path)
    {
        return File.Exists(_dataPath + path);
    }
}

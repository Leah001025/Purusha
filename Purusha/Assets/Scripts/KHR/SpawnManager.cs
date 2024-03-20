using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : SingleTon<SpawnManager>
{
    //특정한 문자열로 된 키값을 입력하면 트랜스폼 값  반환
    public List<Transform> SpawnPoints;

    private void Awake()
    {

    }
    public void SettingSpawnPoints()
    {
        GameObject spawnPointsObject = new GameObject("SpawnPoints");
      
        SpawnPoints.Add(ResourceManager.Instance.Instantiate("Monster/Stage1-1_SpawnPoint", spawnPointsObject.transform).transform);
        SpawnPoints.Add(ResourceManager.Instance.Instantiate("Monster/Stage1-2_SpawnPoint", spawnPointsObject.transform).transform);
        SpawnPoints.Add(ResourceManager.Instance.Instantiate("Monster/Stage1-3_SpawnPoint", spawnPointsObject.transform).transform);
        
    }
}

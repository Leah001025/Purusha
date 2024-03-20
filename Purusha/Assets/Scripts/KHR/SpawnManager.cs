using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : SingleTon<SpawnManager>
{
    //특정한 문자열로 된 키값을 입력하면 트랜스폼 값  반환
    public Dictionary<string, Transform>SpawnPoints = new Dictionary<string, Transform>();
 
    public void SettingSpawnPoints()
    {
        ////스폰포인트 3개 동적
        //GameObject spawnPoints = Instantiate(gameObject);
        SpawnPoints.Add("Stage1 - 1", Resources.Load<Transform>("Prefabs/Monster/Stage1-1_SpawnPoint"));
        //SpawnPoints.Add("Stage1 -2", Resources.Load<Transform>("Prefabs/Monster/Stage1-2_SpawnPoint"));
        //SpawnPoints.Add("Stage1 -3", Resources.Load<Transform>("Prefabs/Monster/Stage1-3_SpawnPoint"));

        //foreach (KeyValuePair<string, Transform> spawnPoint in SpawnPoints)
        //{
        //    spawnPoint.Value.parent = spawnPoints.transform;
        //    Debug.Log("이건말도안돼");

        //}
    }
}

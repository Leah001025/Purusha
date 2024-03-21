using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : SingleTon<SpawnManager>
{
    //특정한 문자열로 된 키값을 입력하면 트랜스폼 값  반환
    public List<Transform> Stage1_SpawnPoints;
    public GameObject SpawnPointsObject;
    private void Awake()
    {
        Stage1_SpawnPoints = new List<Transform>();

    }

    //
    public void SettingSpawnPoints()
    {
        SpawnPointsObject = new GameObject("SpawnPoints");
        // 생성된 몬스터를 스폰 위치의 트랜스폼으로 생성될 수 있게 만들기 

        // Stage1-1_SpawnPoint 프리팹 리스트에 추가
        Transform spawnPoint1 = ResourceManager.Instance.Instantiate("Monster/Stage1-1_SpawnPoint", SpawnPointsObject.transform).transform;
        Stage1_SpawnPoints.Add(spawnPoint1);

        // Stage1-2_SpawnPoint 프리팹 리스트에 추가
        Transform spawnPoint2 = ResourceManager.Instance.Instantiate("Monster/Stage1-2_SpawnPoint", SpawnPointsObject.transform).transform;
        Stage1_SpawnPoints.Add(spawnPoint2);


        // Stage1-3_SpawnPoint 프리팹 리스트에 추가
        Transform spawnPoint3 = ResourceManager.Instance.Instantiate("Monster/Stage1-3_SpawnPoint", SpawnPointsObject.transform).transform;
        Stage1_SpawnPoints.Add(spawnPoint3);

    }

    //스테이지 1에 필요한 몬스터들 소환
    public void SpawnStage1_Monsters()
    {
        ResourceManager.Instance.Instantiate("Monster/Drone", Stage1_SpawnPoints[0]);
        ResourceManager.Instance.Instantiate("Monster/Drone_1", Stage1_SpawnPoints[1]);
        ResourceManager.Instance.Instantiate("Monster/Drone_2", Stage1_SpawnPoints[2]);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : SingleTon<SpawnManager>
{
    //Ư���� ���ڿ��� �� Ű���� �Է��ϸ� Ʈ������ ��  ��ȯ
    public List<Transform> SpawnPoints;

    private void Awake()
    {
        SpawnPoints = new List<Transform>();
    }
    public void SettingSpawnPoints()
    {
        GameObject spawnPointsObject = new GameObject("SpawnPoints");

        // Stage1-1_SpawnPoint ������ ����Ʈ�� �߰�
        SpawnPoints.Add(ResourceManager.Instance.Instantiate("Monster/Stage1-1_SpawnPoint", spawnPointsObject.transform).transform);

        // Stage1-2_SpawnPoint ������ ����Ʈ�� �߰�
        SpawnPoints.Add(ResourceManager.Instance.Instantiate("Monster/Stage1-2_SpawnPoint", spawnPointsObject.transform).transform);


        // Stage1-3_SpawnPoint ������ ����Ʈ�� �߰�
        SpawnPoints.Add(ResourceManager.Instance.Instantiate("Monster/Stage1-3_SpawnPoint", spawnPointsObject.transform).transform);


    }
}

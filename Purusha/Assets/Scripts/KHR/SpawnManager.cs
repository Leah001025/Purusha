using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : SingleTon<SpawnManager>
{
    //Ư���� ���ڿ��� �� Ű���� �Է��ϸ� Ʈ������ ��  ��ȯ
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
        // ������ ���͸� ���� ��ġ�� Ʈ���������� ������ �� �ְ� ����� 

        // Stage1-1_SpawnPoint ������ ����Ʈ�� �߰�
        Transform spawnPoint1 = ResourceManager.Instance.Instantiate("Monster/Stage1-1_SpawnPoint", SpawnPointsObject.transform).transform;
        Stage1_SpawnPoints.Add(spawnPoint1);

        // Stage1-2_SpawnPoint ������ ����Ʈ�� �߰�
        Transform spawnPoint2 = ResourceManager.Instance.Instantiate("Monster/Stage1-2_SpawnPoint", SpawnPointsObject.transform).transform;
        Stage1_SpawnPoints.Add(spawnPoint2);


        // Stage1-3_SpawnPoint ������ ����Ʈ�� �߰�
        Transform spawnPoint3 = ResourceManager.Instance.Instantiate("Monster/Stage1-3_SpawnPoint", SpawnPointsObject.transform).transform;
        Stage1_SpawnPoints.Add(spawnPoint3);

    }

    //�������� 1�� �ʿ��� ���͵� ��ȯ
    public void SpawnStage1_Monsters()
    {
        ResourceManager.Instance.Instantiate("Monster/Drone", Stage1_SpawnPoints[0]);
        ResourceManager.Instance.Instantiate("Monster/Drone_1", Stage1_SpawnPoints[1]);
        ResourceManager.Instance.Instantiate("Monster/Drone_2", Stage1_SpawnPoints[2]);
    }
}

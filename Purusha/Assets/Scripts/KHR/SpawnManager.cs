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
    }
    public void SettingSpawnPoints()
    {
        GameObject spawnPoints = new GameObject("SpawnPoints");
        ResourceManager.Instance.Instantiate("Monster/Stage1-1_SpawnPoint", spawnPoints.transform);
        ResourceManager.Instance.Instantiate("Monster/Stage1-2_SpawnPoint", spawnPoints.transform);
        ResourceManager.Instance.Instantiate("Monster/Stage1-3_SpawnPoint", spawnPoints.transform);

    }
}

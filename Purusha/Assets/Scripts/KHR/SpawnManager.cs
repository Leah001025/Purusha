using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : SingleTon<SpawnManager>
{
    //Ư���� ���ڿ��� �� Ű���� �Է��ϸ� Ʈ������ ��  ��ȯ
    public Dictionary<string, Transform>SpawnPoints = new Dictionary<string, Transform>();
 
    public void SettingSpawnPoints()
    {
        ////��������Ʈ 3�� ����
        //GameObject spawnPoints = Instantiate(gameObject);
        SpawnPoints.Add("Stage1 - 1", Resources.Load<Transform>("Prefabs/Monster/Stage1-1_SpawnPoint"));
        //SpawnPoints.Add("Stage1 -2", Resources.Load<Transform>("Prefabs/Monster/Stage1-2_SpawnPoint"));
        //SpawnPoints.Add("Stage1 -3", Resources.Load<Transform>("Prefabs/Monster/Stage1-3_SpawnPoint"));

        //foreach (KeyValuePair<string, Transform> spawnPoint in SpawnPoints)
        //{
        //    spawnPoint.Value.parent = spawnPoints.transform;
        //    Debug.Log("�̰Ǹ����ȵ�");

        //}
    }
}

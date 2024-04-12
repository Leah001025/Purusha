using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawnController : MonoBehaviour
{
    [Header("SpawnPoint")]
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private Transform monsterSpawnPoint1;
    [SerializeField] private Transform monsterSpawnPoint2;
    [SerializeField] private Transform monsterSpawnPoint3;
    [SerializeField] private GameObject clearPortal;

    private int spawnCount;

    private void FixedUpdate()
    {
        if(GameManager.Instance.wave3Clear)
        {
            clearPortal.SetActive(true);
        }
    }
    public GameObject PlayerSpawn(GameObject player)
    {
        var _player = Instantiate(player, playerSpawnPoint);
        _player.transform.rotation = Quaternion.Euler(0, 180, 0);
        return _player;
    }
    public void MonsterSpawn(GameObject monster, int wave)
    {
        spawnCount++;
        if (spawnCount == 1)
        {
            if (!GameManager.Instance.wave1Clear)
            {
                var _obj = Instantiate(monster, monsterSpawnPoint1);
                _obj.name = wave.ToString();
            }
            else playerSpawnPoint.transform.position = monsterSpawnPoint1.transform.position;
        }
        else if (spawnCount == 2)
        {
            if (!GameManager.Instance.wave2Clear)
            {
                var _obj = Instantiate(monster, monsterSpawnPoint2);
                _obj.name = wave.ToString();
            }
            else playerSpawnPoint.transform.position = monsterSpawnPoint2.transform.position;
        }
        else if (spawnCount == 3)
        {
            if (!GameManager.Instance.wave3Clear)
            {
                var _obj = Instantiate(monster, monsterSpawnPoint3);
                _obj.name = wave.ToString();
            }
            else playerSpawnPoint.transform.position = monsterSpawnPoint3.transform.position;
        }
    }
}

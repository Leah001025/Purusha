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

    public GameObject PlayerSpawn(GameObject player)
    {
        var _player = Instantiate(player, playerSpawnPoint);
        _player.transform.rotation = Quaternion.Euler(0, 180, 0);
        return _player;
    }
    public void MonsterSpawn(GameObject monster, int wave)
    {
        if (monsterSpawnPoint1.childCount == 0)
        {
            var _obj = Instantiate(monster, monsterSpawnPoint1);
            _obj.name = wave.ToString();
        }
        else if (monsterSpawnPoint2.childCount == 0)
        {
            var _obj = Instantiate(monster, monsterSpawnPoint2);
            _obj.name = wave.ToString();
        }
        else
        {
            var _obj = Instantiate(monster, monsterSpawnPoint3);
            _obj.name = wave.ToString();
        }
    }
}

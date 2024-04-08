using Cinemachine;
using Enums;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager instance = null;
    public SpawnManager()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }
    public static SpawnManager Instance { get { if (instance == null) return null; return instance; } }

    [SerializeField] private Transform mapSpawnPoint;
    public Canvas battleCanvas;

    private int stageID;
    private StageDataBase stageDB;
    private WaveDataBase waveDB;
    private EnemyDataBase enemyDB;

    private WaveData waveData;

    private MapSpawnController mapSpawnController;
    private PlayerConeRaycastDetection raycastDetection;

    private GameObject player;

    public Image effectImage;

    private void Awake()
    {
        stageID = GameManager.Instance.stageID;

        stageDB = DataManager.Instance.StageDB;
        waveDB = DataManager.Instance.WaveDB;
        enemyDB = DataManager.Instance.EnemyDB;

        var resources = Resources.Load(stageDB.GetData(stageID).OpenMapPath) as GameObject;
        var _map = Instantiate(resources, mapSpawnPoint);
        mapSpawnController = _map.GetComponent<MapSpawnController>();
    }
    private void Start()
    {
        MonsterSpawn();
        PlayerSpawm();
    }
    private void PlayerSpawm()
    {
        player = Resources.Load("Prefabs/Player/WorldPlayer") as GameObject;
        var _obj = mapSpawnController.PlayerSpawn(player);
        for (int i = 1; 5 >= i; i++)
        {
            if (GameManager.Instance.User.teamData.ContainsKey(i))
            {
                var _playerRsc = Resources.Load(GameManager.Instance.User.teamData[i].status.prefabPath) as GameObject;
                var _playerObj = Instantiate(_playerRsc, _obj.transform.GetChild(0).transform);
                _playerObj.GetComponent<CharacterTurnController>().enabled = false;
                break;
            }
        }
        raycastDetection = _obj.GetComponentInChildren<PlayerConeRaycastDetection>();
        raycastDetection.battleEffect = effectImage;
    }
    private void MonsterSpawn()
    {
        foreach (StageWave stageWave in stageDB.GetData(stageID).StageWaves)
        {
            waveData = waveDB.GetData(stageWave._waveID);
            var enemyID = waveData.Enemys[waveData.Enemys.Count - 1]._enemyID;
            var resources = Resources.Load(enemyDB.GetData(enemyID).PrefabPath) as GameObject;
            mapSpawnController.MonsterSpawn(resources, stageWave._waveID);
        }
    }
}

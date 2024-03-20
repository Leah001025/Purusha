using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSetting : MonoBehaviour
{
    private void Awake()
    {
        SpawnManager.Instance.SettingSpawnPoints();
    }
}

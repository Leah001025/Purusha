using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class MapSetting : MonoBehaviour
{
    private void Awake()
    {
        SpawnManager.Instance.SettingSpawnPoints();

    }

    
}

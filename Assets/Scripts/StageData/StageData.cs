using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class StageData : DataBase<WaveData>
{
    [SerializeField] private int _id;
    [SerializeField] private string _openMapPath;
    [SerializeField] private int _waveID_1;
    [SerializeField] private int _waveID_2;
    [SerializeField] private int _waveID_3;

    public int ID => _id;
    public string OpenMapPath => _openMapPath;

    private List<StageWave> _stageWaves;

    public List<StageWave> StageWaves
    {
        get 
        {
            if (_stageWaves == null)
            {
                _stageWaves = new List<StageWave>();

                AddWaves(_waveID_1);
                AddWaves(_waveID_2);
                AddWaves(_waveID_3);
            }
            return _stageWaves;
        }
    }
    private void AddWaves(int waveID)
    {
        if (waveID != 0)
        {
            _stageWaves.Add(new StageWave(waveID));
        }
    }
}
public class StageWave
{
    public int _waveID { get; }

    public StageWave(int waveID)
    {
        _waveID = waveID;
    }
}

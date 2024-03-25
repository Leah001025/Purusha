using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageData : DataBase<StageData>
{
    [SerializeField] private int _id;

    [SerializeField] private int _enemyID_1;
    [SerializeField] private int _enemyCount_1;
    [SerializeField] private int _enemyID_2;
    [SerializeField] private int _enemyCount_2;
    [SerializeField] private int _enemyID_3;
    [SerializeField] private int _enemyCount_3;
    [SerializeField] private int _enemyID_4;
    [SerializeField] private int _enemyCount_4;
    [SerializeField] private int _enemyID_5;
    [SerializeField] private int _enemyCount_5;

    [SerializeField] private int _compensation_1;
    [SerializeField] private int _compensationCount_1;
    [SerializeField] private int _compensation_2;
    [SerializeField] private int _compensationCount_2;
    [SerializeField] private int _compensation_3;
    [SerializeField] private int _compensationCount_3;

    public int ID => _id;

    private List<Enemys> _enemys;
    private List<Compensations> _compensations;

    public List<Enemys> Enemys
    {
        get 
        {
            if(_enemys == null)
            {
                _enemys = new List<Enemys>();

                AddEnemys(_enemyID_1, _enemyCount_1);
                AddEnemys(_enemyID_2, _enemyCount_2);
                AddEnemys(_enemyID_3, _enemyCount_3);
                AddEnemys(_enemyID_4, _enemyCount_4);
                AddEnemys(_enemyID_5, _enemyCount_5);
            }
            return _enemys;
        }
    }
    private void AddEnemys(int enemyID, int enemyCount)
    {
        if (enemyID != 0)
        {
            _enemys.Add(new Enemys(enemyID, enemyCount));
        }
    }
    public List<Compensations> Compensations
    {
        get
        {
            if (_compensations == null)
            {
                _compensations = new List<Compensations>();

                AddCompensations(_compensation_1, _compensationCount_1);
                AddCompensations(_compensation_2, _compensationCount_2);
                AddCompensations(_compensation_3, _compensationCount_3);
            }
            return _compensations;
        }
    }
    private void AddCompensations(int compensation, int compensationCount)
    {
        if (compensation != 0)
        {
            _compensations.Add(new Compensations(compensation, compensationCount));
        }
    }
}
public class Enemys
{
    public int _enemyID { get; }
    public int _enemyCount { get; }

    public Enemys(int enemyID, int enemyCount)
    {
        _enemyID = enemyID;
        _enemyCount = enemyCount;
    }
}
public class Compensations
{
    public int _compensation { get; }
    public int _compensationCount { get; }

    public Compensations(int compensation, int enemyCount)
    {
        _compensation = compensation;
        _compensationCount = enemyCount;
    }
}

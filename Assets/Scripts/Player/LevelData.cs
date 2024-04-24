using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    [SerializeField] private int _level;
    [SerializeField] private int _exp;

    public int Level { get { return _level; } set { _level = value; } }
    public int Exp { get { return _exp; } set { _exp = value; } }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class DataBase<T>
{
    protected Dictionary<int, T> _data = new Dictionary<int, T>();
    public virtual T GetData(int key)
    {
        if (_data.ContainsKey(key))
            return _data[key];
        return default;
    }
}

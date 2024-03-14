using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class DataBase<T>
{
    public abstract void SetData(T metaData);
}

public abstract class DataBaseList<T1, T2>
{
    public Dictionary<T1, T2> datas = new Dictionary<T1, T2>();

    public abstract void SetData(List<T2> metaDataList);
}



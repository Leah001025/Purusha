using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargerEffect : MonoBehaviour
{
    
    void Start()
    {
        transform.position = BattleManager.Instance.target.transform.position+new Vector3(0, 1.5f, 0);
    }


}

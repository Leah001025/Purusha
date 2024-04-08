using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSlot : MonoBehaviour
{
    public void StageSelect()
    {
        GameManager.Instance.stageID = int.Parse(gameObject.name);
    }
}

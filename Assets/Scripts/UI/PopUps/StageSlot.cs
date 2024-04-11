using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSlot : MonoBehaviour
{
    public void StageSelect()
    {
        SoundManager.Instance.ButtonAudio("BasicMenuO_1");
        GameManager.Instance.stageID = int.Parse(gameObject.name);
    }
}

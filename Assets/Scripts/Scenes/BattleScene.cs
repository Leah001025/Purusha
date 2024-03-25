using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene : BaseScene
{
    protected override void Awake()
    {
        base.Awake();
        Cursor.lockState = CursorLockMode.None;
    }
    
}

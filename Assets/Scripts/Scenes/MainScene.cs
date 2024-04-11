using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    private void Start()
    {
        SoundManager.Instance.SceneAudioStart("MainScene");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : BaseScene
{
    private void Start()
    {
        SoundManager.Instance.SceneAudioStart("StartScene");
    }
}

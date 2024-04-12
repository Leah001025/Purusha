using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : BaseScene
{
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        SoundManager.Instance.SceneAudioStart("StartScene");
    }
}

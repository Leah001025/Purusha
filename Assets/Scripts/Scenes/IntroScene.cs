using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScene : BaseScene
{
    public void Start()
    {
        GameManager.Instance.stageID = 0000;
        SoundManager.Instance.BgmAudio("Intro");
    }
    public void IntroSceneEnd()
    {
        GameManager.Instance.User.introScene = true;
        SceneLoadManager.Instance.ChangeScene("MainScene");
    }
}

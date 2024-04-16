using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScene : BaseScene
{
    public void IntroSceneEnd()
    {
        GameManager.Instance.User.introScene = true;
        SceneLoadManager.Instance.ChangeScene("Main");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : BaseScene
{
    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "MainScene")
        {
            SoundManager.Instance.SceneAudioStart("MainScene");
        }
        UIManager.Instance.charInventoryUI.Clear();
        UIManager.Instance.popups.Clear();
    }
}

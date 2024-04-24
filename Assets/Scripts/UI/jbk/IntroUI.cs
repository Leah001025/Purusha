using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class IntroUI : MonoBehaviour
{
    public Image backGround;
    public Image startButton;
    public Image exitButton;
    public Image gameBytton;

    public GameObject gameInfo;

    private WaitForSeconds effectWaitFor;
    private WaitForSeconds startWaitFor;

    public void Awake()
    {
        ResetStart();
        startWaitFor = new WaitForSeconds(3f);
        effectWaitFor = new WaitForSeconds(0.1f);
        StartCoroutine(StartSceneEffect());
    }
    public void GameStart()
    {
        SoundManager.Instance.ButtonAudio("BasicMenuO_1");
        StartCoroutine(LoadData());
    }

    public void ExitGame()
    {
        SoundManager.Instance.ButtonAudio("BasicMenuO_1");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit();
#endif
    }
    public void GameInfo() 
    {
        gameInfo.SetActive(true);
    }
    public void EndInfo()
    {
        gameInfo.SetActive(false);
    }
    IEnumerator StartSceneEffect()
    {
        while (true)
        {
            float effectTime = 2.5f;
            while (effectTime > 0)
            {
                yield return effectWaitFor;
                backGround.material.SetFloat("_Intensity", Random.Range(2, 10));
                backGround.material.SetFloat("_ValueX", Random.Range(-0.5f, 0.5f));
                startButton.color = new Color(0.5f, Random.Range(0, 0.2f), 1, Random.Range(0f, 1f));
                exitButton.color = new Color(0.5f, Random.Range(0, 0.2f), 1, Random.Range(0f, 1f));
                gameBytton.color = new Color(0.5f, Random.Range(0, 0.2f), 1, Random.Range(0f, 1f));
                effectTime -= 0.1f;
            }
            ResetStart();
            yield return startWaitFor;
        }
    }
    private void ResetStart()
    {
        backGround.material.SetFloat("_Intensity", 0);
        backGround.material.SetFloat("_ValueX", 0);
        startButton.color = new Color(1, 1, 1, 1);
        exitButton.color = new Color(1, 1, 1, 1);
        gameBytton.color = new Color(1, 1, 1, 1);
    }
    private IEnumerator LoadData()
    {
        yield return new WaitForSeconds(0.2f);
        GameManager.Instance.LoadDatas();
        yield return new WaitForSeconds(0.2f);
        if (GameManager.Instance.User.introScene)
            SceneLoadManager.Instance.LoadingChangeScene("MainScene");
        else
            SceneLoadManager.Instance.LoadingChangeScene("IntroScene");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
    [SerializeField] private Image _loadingBar;
    [SerializeField] private string _nextSceneName;

    private void Awake()
    {
        _nextSceneName = SceneLoadManager.Instance.NextSceneName;
    }
    public void Start()
    {
        StartCoroutine(LoadSceneProcess());
        SoundManager.Instance.SceneAudioExit();
    }


    private IEnumerator LoadSceneProcess()
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(_nextSceneName);
        loadScene.allowSceneActivation = false;

        float timer = 0f;
        while (!loadScene.isDone)
        {
            yield return null;

            if (loadScene.progress < 0.5f)
            {
                _loadingBar.fillAmount = loadScene.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                _loadingBar.fillAmount = Mathf.Lerp(0.5f, 1f, timer);

            }
            if (_loadingBar.fillAmount >= 1f)
            {
                //SceneLoadManager NowScene Update
                SceneLoadManager.Instance.NowSceneName = _nextSceneName;
                loadScene.allowSceneActivation = true;
            }
        }        
        yield return null;
    }
}

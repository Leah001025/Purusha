using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
    [SerializeField] private Slider _loadingBar;
    [SerializeField] private string _nextSceneName;

    private void Awake()
    {
        _nextSceneName = SceneLoadManager.Instance.NextSceneName;
    }
    public void Start()
    {
        StartCoroutine(LoadSceneProcess());
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
                _loadingBar.value = loadScene.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                _loadingBar.value = Mathf.Lerp(0.5f, 1f, timer);
             
            }
            if(_loadingBar.value >=1f)
            {
                loadScene.allowSceneActivation = true;
            }
        }
        yield return null;
    }
}
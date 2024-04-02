using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : BaseScene
{
    // 로딩할 씬의 이름

    public string sceneToload;

    public void Start()
    {
        //로딩할 씬을 비동기로 로드하기
        StartCoroutine(LoadSceneAsync());
    }

    public IEnumerator LoadSceneAsync()
    {
        // 비동기로 씬을 로드
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToload);
        // 로딩 진행 상황을 표시 하는 UI 등 업데이트
        while(!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress/0.9f );
            //로딩 진행도를 0부터 1 사이로 정수화 시킴
            Debug.Log("로딩 진행도" + (progress * 100) + "%");
            //로딩 진행도를 표시하는 UI 업데이트 등을 수행

            yield return null;
        }

        //로딩 완료되면 다음 씬으로 이동
        Debug.Log("로딩 완료, 다음 화면으로 이동합니다. ");

        // 로딩씬 업로드
        SceneManager.UnloadSceneAsync("LoadingScene");
    }
}

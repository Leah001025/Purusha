using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class SceneLoadManager : SingleTon<SceneLoadManager>
{
    public bool isDontDestroy = false;

    public string NowSceneName = "";
    public string NextSceneName = "";
    protected override void Awake()
    {
        base.Awake();

        NowSceneName = SceneManager.GetActiveScene().name;
    }

    public async void ChangeScene(string SceneName, Action callback = null, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {
        var op = SceneManager.LoadSceneAsync(SceneName, loadSceneMode);

        while (!op.isDone)
        {
            await Task.Yield();
        }

        if (loadSceneMode == LoadSceneMode.Single)
            NowSceneName = SceneName;

        callback?.Invoke();
    }

    public async void UnLoadScene(string SceneName, Action callback = null)
    {
        var op = SceneManager.UnloadSceneAsync(SceneName);

        while (!op.isDone)
        {
            await Task.Yield();
        }

        callback?.Invoke();

    }

        // 씬이 이동할시 , 로딩씬이 필요한경우 해당 함수의 매개변수의 인자값으로 씬 이름을 넣어주시면 됩니다!
        // 로딩씬 거친 후에 다음씬으로 넘어가짐(비동기로)
        //LoadingChangeScene("SceneName"); 이리 적어주시면 되요!
    public async void LoadingChangeScene(string SceneName)
    {
         NextSceneName = SceneName;
        ChangeScene("LoadingScene");
    }
}

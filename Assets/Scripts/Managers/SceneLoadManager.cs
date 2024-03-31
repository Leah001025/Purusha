using System;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class SceneLoadManager : SingleTon<SceneLoadManager>
{
    public bool isDontDestroy = false;

    public int stageID;

    public string NowSceneName = "";
    public string NextSceneName = "";
    public DataList DataList_;
    protected override void Awake()
    {
        DataList_ = Resources.Load<DataList>("KCH/DataBase/DataList");
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

        // ���� �̵��ҽ� , �ε����� �ʿ��Ѱ�� �ش� �Լ��� �Ű������� ���ڰ����� �� �̸��� �־��ֽø� �˴ϴ�!
        // �ε��� ��ģ �Ŀ� ���������� �Ѿ��(�񵿱��)
        //LoadingChangeScene("SceneName"); �̸� �����ֽø� �ǿ�!
    public async void LoadingChangeScene(string SceneName)
    {
         NextSceneName = SceneName;
        ChangeScene("LoadingScene");
    }


    
}

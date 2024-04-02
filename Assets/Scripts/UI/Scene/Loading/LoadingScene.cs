using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : BaseScene
{
    // �ε��� ���� �̸�

    public string sceneToload;

    public void Start()
    {
        //�ε��� ���� �񵿱�� �ε��ϱ�
        StartCoroutine(LoadSceneAsync());
    }

    public IEnumerator LoadSceneAsync()
    {
        // �񵿱�� ���� �ε�
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToload);
        // �ε� ���� ��Ȳ�� ǥ�� �ϴ� UI �� ������Ʈ
        while(!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress/0.9f );
            //�ε� ���൵�� 0���� 1 ���̷� ����ȭ ��Ŵ
            Debug.Log("�ε� ���൵" + (progress * 100) + "%");
            //�ε� ���൵�� ǥ���ϴ� UI ������Ʈ ���� ����

            yield return null;
        }

        //�ε� �Ϸ�Ǹ� ���� ������ �̵�
        Debug.Log("�ε� �Ϸ�, ���� ȭ������ �̵��մϴ�. ");

        // �ε��� ���ε�
        SceneManager.UnloadSceneAsync("LoadingScene");
    }
}

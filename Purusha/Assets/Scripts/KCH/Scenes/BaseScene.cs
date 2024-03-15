using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    //isDontDestroy�� false�� ��� DontDestroy�� Additive���� �ε�
    //�ݹ����� isDontDestroy true�� ���� �� DontDestroy�� ��ε�
    protected virtual void Awake()
    {
        if (!SceneLoadManager.Instance.isDontDestroy)
        {
            SceneLoadManager.Instance.ChangeScene("DontDestroy", () =>
            {
                SceneLoadManager.Instance.isDontDestroy = true;
                SceneLoadManager.Instance.UnLoadScene("DontDestroy");
            }, UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }
    }
}

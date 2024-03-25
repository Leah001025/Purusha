using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    //isDontDestroy가 false인 경우 DontDestroy씬 Additive모드로 로드
    //콜백으로 isDontDestroy true로 변경 후 DontDestroy씬 언로드
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

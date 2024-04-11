using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _clearBtn;
    public void Enter()
    {
        _clearBtn.SetActive(true);
    }

    public void Exit()
    {
        _clearBtn.SetActive(false);
    }

    public void Interaction()
    {
        SceneLoadManager.Instance.LoadingChangeScene("Main");
    }
}

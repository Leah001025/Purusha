using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Portal : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _clearBtn;
    private StageInfo stageInfo;
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
        StageClear();
        SceneLoadManager.Instance.LoadingChangeScene("Main");
    }
    private void StageClear()
    {
        stageInfo = new StageInfo();

        stageInfo.stageID = GameManager.Instance.stageID;
        stageInfo.wave1Clear = GameManager.Instance.wave1Clear;
        stageInfo.wave2Clear = GameManager.Instance.wave2Clear;
        stageInfo.wave3Clear = GameManager.Instance.wave3Clear;

        if (GameManager.Instance.User.stageClear.Count != 0)
        {
            if (stageInfo.stageID != GameManager.Instance.User.stageClear.Peek().stageID)
            {
                GameManager.Instance.User.stageClear.Push(stageInfo);
            }
            else
            {
                GameManager.Instance.User.stageClear.Peek().wave1Clear = GameManager.Instance.wave1Clear;
                GameManager.Instance.User.stageClear.Peek().wave2Clear = GameManager.Instance.wave2Clear;
                GameManager.Instance.User.stageClear.Peek().wave3Clear = GameManager.Instance.wave3Clear;
            }
        }
    }
}

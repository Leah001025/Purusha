using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Portal : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _clearBtn;
    private StageInfo stageInfo;
    private bool wave1Clear;
    private bool wave2Clear;
    private bool wave3Clear;

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
        SceneLoadManager.Instance.LoadingChangeScene("OpenWorldScene");
    }
    private void StageClear()
    {
        wave1Clear = GameManager.Instance.wave1Clear;
        wave2Clear = GameManager.Instance.wave2Clear;
        wave3Clear = GameManager.Instance.wave3Clear;

        if (GameManager.Instance.User.stageClear.Count != 0)
        {
            GameManager.Instance.User.stageClear.Peek().wave1Clear = wave1Clear;
            GameManager.Instance.User.stageClear.Peek().wave2Clear = wave2Clear;
            GameManager.Instance.User.stageClear.Peek().wave3Clear = wave3Clear;
        }
        //wave3 클리어시 waveInfo 초기화
        if (wave1Clear && wave2Clear && wave3Clear)
        {
            GameManager.Instance.ResetWaveInfo();
            int nextStage = GameManager.Instance.User.NextStage();
            GameManager.Instance.stageID = nextStage;
            StageInfo nextStageInfo = new StageInfo();
            nextStageInfo.stageID = nextStage;
            GameManager.Instance.User.stageClear.Push(nextStageInfo);
            GameManager.Instance.User.isCutScenePlay = false;
            GameManager.Instance.User.ResetCharacterHP();
        }
    }
}

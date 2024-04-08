using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterSeletPopUp : UIBase
{
    [SerializeField] private TMP_Text _chapterName;
    [SerializeField] private TMP_Text _chapterDesc;
    [SerializeField] private GameObject _chapterInfo;
    [SerializeField] private GameObject _stage;
    [SerializeField] private GameObject _stageInfo;

    private int chapterNum;

    public void ShowChapterData(string chapterNumber)
    {
        ChapterData chapterData = DataManager.Instance.ChapterDB.GetData(int.Parse(chapterNumber));


        if (chapterData != null)
        {
            chapterNum = int.Parse(chapterNumber);
            _chapterInfo.SetActive(true);
            _chapterName.text = $"{chapterNumber.Substring(chapterNumber.Length - 1, 1)}. {chapterData.ChapterName}";
            _chapterDesc.text = $"{chapterData.Description}";
            _stage.SetActive(false);
        }
    }
    public void CloseChpaterInfo() // 챕터 정보 닫기
    {
        _chapterInfo.SetActive(false);
    }

    public void OpenStageInfo() // 스테이지 창 열기
    {
        ChapterData chapterData = DataManager.Instance.ChapterDB.GetData(chapterNum);

        _stage.SetActive(true);
        for (int i = 1; chapterData.StageCount >= i; i++)
        {
            var _rsc = Resources.Load("Prefabs/Chapters/Stages") as GameObject;
            var _obj = Instantiate(_rsc, _stageInfo.transform);
            _obj.name = $"{chapterNum}0{i}";
            if (GameManager.Instance.User.stageClear != null)
            {
                if (GameManager.Instance.User.stageClear.Peek().wave3Clear == false)
                {
                    GameManager.Instance.stageID = int.Parse(_obj.name);
                }
            }
        }
    }

    public void CloseStageInfo() // 스테이지 창 닫기
    {
        if (_stageInfo.transform.childCount != 0)
        {
            for(int i = 0; _stageInfo.transform.childCount > i; i++)
            {
                Destroy(_stageInfo.transform.GetChild(i).gameObject);
            }
        }
        _stage.SetActive(false);
    }

    public void BattleStart()
    {
        SceneLoadManager.Instance.LoadingChangeScene("Dev_Main_Scene");
    }
}
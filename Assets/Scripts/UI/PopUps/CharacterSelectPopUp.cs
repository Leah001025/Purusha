using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSeletPopUp : UIBase
{
    [SerializeField] private TMP_Text _chapterName;
    [SerializeField] private TMP_Text _chapterDesc;
    [SerializeField] private GameObject _chapterInfo;
    [SerializeField] private GameObject _stage;
    [SerializeField] private GameObject _stageInfo;
    private Stack<StageInfo> userStageInfo;
    private int gmStageID;

    private int chapterNum;

    public void ShowChapterData(string chapterNumber)
    {
        SoundManager.Instance.ButtonAudio("BasicMenuO_1");
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
    public void CloseChpaterInfo() // é�� ���� �ݱ�
    {
        SoundManager.Instance.ButtonAudio("BasicMenuC_1");
        CloseStageInfo(); //é�� ���� ������ �������� â�� ���� ������
        _chapterInfo.SetActive(false);
    }

    public void OpenStageInfo() // �������� â ����
    {
        SoundManager.Instance.ButtonAudio("BasicMenuO_1");
        userStageInfo = GameManager.Instance.User.stageClear;
        ChapterData chapterData = DataManager.Instance.ChapterDB.GetData(chapterNum);
        if (_stage.activeSelf == true) return;
        _stage.SetActive(true);
        for (int i = 1; chapterData.StageCount >= i; i++)
        {
            var _rsc = Resources.Load("Prefabs/Chapters/Stages") as GameObject;
            var _obj = Instantiate(_rsc, _stageInfo.transform);
            _obj.name = $"{chapterNum}0{i}";
            Image img = _obj.GetComponent<Image>();
            //Ŭ���� �������� �Ķ���
            if (userStageInfo.Peek().stageID > int.Parse(_obj.name))
            {
                img.color = Color.blue;
            }
            if (userStageInfo.Peek().stageID == int.Parse(_obj.name))
            {
                if (userStageInfo.Peek().wave3Clear == true) 
                {
                    img.color = Color.blue;
                }
            }
            if (userStageInfo != null)
            {
                if (userStageInfo.Peek().wave3Clear == false)
                {
                    GameManager.Instance.stageID = int.Parse(_obj.name);
                }
            }
        }
    }

    public void CloseStageInfo() // �������� â �ݱ�
    {
        SoundManager.Instance.ButtonAudio("BasicMenuC_1");
        if (_stageInfo.transform.childCount != 0)
        {
            for (int i = 0; _stageInfo.transform.childCount > i; i++)
            {
                Destroy(_stageInfo.transform.GetChild(i).gameObject);
            }
        }
        _stage.SetActive(false);
    }

    public void BattleStart()
    {
        userStageInfo = GameManager.Instance.User.stageClear;
        gmStageID = GameManager.Instance.stageID;
        //�̹� Ŭ������ �������� ���� �Ұ�
        if (userStageInfo.Peek().stageID > gmStageID) return;
        if (userStageInfo.Peek().stageID == gmStageID)
        {
            if (userStageInfo.Peek().wave3Clear == true) return;
        }
            SceneLoadManager.Instance.LoadingChangeScene("Dev_Main_Scene");
    }
}
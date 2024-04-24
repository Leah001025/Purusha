using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Enums;
using UnityEditor;

public class ClearUI : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] private GameObject characterInfo;
    [SerializeField] private GameObject itemInfo;
    [SerializeField] private GameObject characterBattleInfo;
    [SerializeField] private GameObject battleInfoContent;

    [Header("Image")]
    [SerializeField] private Image clearImage;
    [SerializeField] private Image clearBG;

    [Header("Text")]
    [SerializeField] private TMP_Text clearText;
    [SerializeField] private TMP_Text stageName;
    [SerializeField] private TMP_Text Times;
    [SerializeField] private TMP_Text Turns;

    private StageDataBase stageDB;
    private WaveDataBase waveDB;
    private GameManager gameManager;
    private StageInfo stageInfo;

    private void Awake()
    {
        stageDB = DataManager.Instance.StageDB;
        waveDB = DataManager.Instance.WaveDB;
    }

    private void Start()
    {
        StageState();
        StageEnd();
        TeamInfo();
        BattleInfo();
        ItemInfo();
    }
    private void Update()
    {

    }
    private void StageState()
    {
        GameManager.Instance.User.UpdateCharacterData();
        switch (BattleManager.Instance.gameState)
        {
            case GameEnd.success:
                clearText.text = "Success";
                clearImage.sprite = Resources.Load<Sprite>("UI/Icon/Success");
                clearBG.sprite = Resources.Load<Sprite>("UI/Icon/SuccessBG");
                break;
            case GameEnd.fail:
                clearText.text = "Fail";
                clearImage.sprite = Resources.Load<Sprite>("UI/Icon/Fail");
                clearBG.sprite = Resources.Load<Sprite>("UI/Icon/FailBG");
                GameManager.Instance.User.ResetCharacterHP();
                stageInfo.wave1Clear = false;
                stageInfo.wave2Clear = false;
                stageInfo.wave3Clear = false;
                break;
        }
    }
    private void StageEnd()
    {
        string waveID = GameManager.Instance.waveID.ToString();
        stageName.text = stageDB.GetData(GameManager.Instance.stageID).StageName + " wave" + waveID.Substring(waveID.Length - 1, 1);
        Times.text = BattleManager.Instance.battleInfo.battleTime.ToString("N2");
        Turns.text = BattleManager.Instance.battleInfo.battleTurn.ToString();
    }
    private void TeamInfo()
    {
        int slotIndex = 0;
        foreach (CharacterData _characterData in GameManager.Instance.User.teamData.Values)
        {
            var _sprite = Resources.Load<Sprite>(_characterData.status.spritePath);
            characterInfo.transform.GetChild(slotIndex).GetComponent<Image>().sprite = _sprite;
            slotIndex++;
        }
    }
    private void BattleInfo()
    {
        for (int i = 1; GameManager.Instance.User.teamData.Count >= i; i++)
        {
            var _resources = Resources.Load("Prefabs/Battle/DamageInfo") as GameObject;
            var _obj = Instantiate(_resources, battleInfoContent.transform);
            _obj.name = i.ToString();
        }
    }
    private void ItemInfo()
    {
        characterBattleInfo.SetActive(false);
        if (BattleManager.Instance.gameState == GameEnd.success)
        {
            foreach (Compensations _compensations in waveDB.GetData(GameManager.Instance.waveID).Compensations)
            {
                var _resources = Resources.Load("Prefabs/Battle/ItemSlot") as GameObject;
                var _obj = Instantiate(_resources, itemInfo.transform);

                var _resourcesSprite = Resources.Load<Sprite>(DataManager.Instance.ItemDB.GetData(_compensations._compensation).SpritePath);
                _obj.GetComponent<Image>().sprite = _resourcesSprite;
                _obj.transform.GetChild(0).GetComponent<TMP_Text>().text = _compensations._compensationCount.ToString();
            }
        }
    }
    public void MainMenuBtn()
    {
        StageClear();
        SceneLoadManager.Instance.LoadingChangeScene("MainScene");
    }
    private void StageClear()
    {
        gameManager = GameManager.Instance;
        stageInfo = new StageInfo();

        stageInfo.stageID = gameManager.stageID;
        stageInfo.wave1Clear = gameManager.wave1Clear;
        stageInfo.wave2Clear = gameManager.wave2Clear;
        stageInfo.wave3Clear = gameManager.wave3Clear;
        if (gameManager.User.stageClear.Count != 0)
        {
            if (stageInfo.stageID != gameManager.User.stageClear.Peek().stageID)
            {
                gameManager.User.stageClear.Push(stageInfo);
            }
            else
            {
                gameManager.User.stageClear.Peek().wave1Clear = gameManager.wave1Clear;
                gameManager.User.stageClear.Peek().wave2Clear = gameManager.wave2Clear;
                gameManager.User.stageClear.Peek().wave3Clear = gameManager.wave3Clear;
            }
        }
        //wave3 클리어시 waveInfo 초기화
        if (gameManager.wave3Clear && gameManager.wave2Clear && gameManager.wave1Clear)
        {
            gameManager.ResetWaveInfo();
            int nextStage = gameManager.User.NextStage();
            StageInfo nextStageInfo = new StageInfo();
            gameManager.User.stageClear.Push(nextStageInfo);
            gameManager.User.isCutScenePlay = false;
            nextStageInfo.stageID = nextStage;
            GameManager.Instance.User.ResetCharacterHP();
        }
    }
    public void ContinueBtn()
    {
        SceneLoadManager.Instance.LoadingChangeScene("OpenWorldScene");
    }
    public void BattleInfoBtn()
    {
        if (characterBattleInfo.activeSelf == false)
        {
            itemInfo.SetActive(false);
            characterBattleInfo.SetActive(true);
        }
        else
        {
            itemInfo.SetActive(true);
            characterBattleInfo.SetActive(false);
        }
    }
}

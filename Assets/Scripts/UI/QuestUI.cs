using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI chapter;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI wave1;
    [SerializeField] private TextMeshProUGUI wave2;
    [SerializeField] private TextMeshProUGUI wave3;
    [SerializeField] private TextMeshProUGUI reward1;
    [SerializeField] private TextMeshProUGUI reward2;
    [SerializeField] private TextMeshProUGUI reward3;

    private int stageID;
    private int chapterID;
    private int wave1ID;
    private int wave2ID;
    private int wave3ID;
    private string enermy1;
    private string enermy2;
    private string enermy3;
    private string item1;
    private string item2;
    private string item3;
    private bool isWave1Clear;
    private bool isWave2Clear;
    private bool isWave3Clear;
    ChapterData chapterData = new ChapterData();
    WaveData wave1Data = new WaveData();
    WaveData wave2Data = new WaveData();
    WaveData wave3Data = new WaveData();

    private void Start()
    {
        Init();
        UpdateUI();
    }
    private void Init()
    {
        stageID = GameManager.Instance.stageID;
        isWave1Clear = GameManager.Instance.wave1Clear;
        isWave2Clear = GameManager.Instance.wave2Clear;
        isWave3Clear = GameManager.Instance.wave3Clear;
        chapterID = int.Parse(stageID.ToString().Substring(0, 2));
        wave1ID = stageID * 100 + 1;
        wave2ID = stageID * 100 + 2;
        wave3ID = stageID * 100 + 3;
        chapterData = DataManager.Instance.ChapterDB.GetData(chapterID);
        wave1Data = DataManager.Instance.WaveDB.GetData(wave1ID);
        wave2Data = DataManager.Instance.WaveDB.GetData(wave2ID);
        wave3Data = DataManager.Instance.WaveDB.GetData(wave3ID);
        enermy1 = DataManager.Instance.EnemyDB.GetData(wave1Data.Enemys[0]._enemyID).Name;
        enermy2 = DataManager.Instance.EnemyDB.GetData(wave2Data.Enemys[0]._enemyID).Name;
        enermy3 = DataManager.Instance.EnemyDB.GetData(wave3Data.Enemys[0]._enemyID).Name;
        if (wave3Data._compensation_1 != 0)
            item1 = DataManager.Instance.ItemDB.GetData(wave3Data._compensation_1).Name;
        if (wave3Data._compensation_2 != 0)
            item2 = DataManager.Instance.ItemDB.GetData(wave3Data._compensation_2).Name;
        if (wave3Data._compensation_3 != 0)
            item3 = DataManager.Instance.ItemDB.GetData(wave3Data._compensation_3).Name;
    }
    private void UpdateUI()
    {
        chapter.text = $"Chap{stageID.ToString().Substring(1, 1)}-{stageID.ToString().Substring(3, 1)}";
        title.text = chapterData.ChapterName;
        if (!isWave1Clear) wave1.color = Color.white; else wave1.color = Color.gray;
        if (!isWave2Clear) wave2.color = Color.white; else wave2.color = Color.gray;
        if (!isWave3Clear) wave3.color = Color.white; else wave3.color = Color.gray;
        wave1.text = $"Wave1 : {enermy1}";
        wave2.text = $"Wave2 : {enermy2}";
        wave3.text = $"Wave3 : {enermy3}";
        if (wave1Data._compensation_1 != 0)
            reward1.text = $"{item1} {wave3Data._compensationCount_1}°³";
        else reward1.text = "";
        if (wave1Data._compensation_2 != 0)
            reward2.text = $"{item2} {wave3Data._compensationCount_2}°³";
        else reward2.text = "";
        if (wave1Data._compensation_3 != 0)
            reward3.text = $"{item3} {wave3Data._compensationCount_3}°³";
        else reward3.text = "";
    }
}

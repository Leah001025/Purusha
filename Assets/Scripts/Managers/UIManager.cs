using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingleTon<UIManager>
{
    public Dictionary<string, UIBase> popups = new Dictionary<string, UIBase>();
    private Dictionary<int, GameObject> battlePlayerStatus = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> battleEnemyStatus = new Dictionary<int, GameObject>();
    public Dictionary<int, Inventory> charInventoryUI = new Dictionary<int, Inventory>();
    public PopupManager popupManager;
    public UpgradeStatUI upgradeStatUI;
    public LevelUpUI levelUpUI;
    public ItemInventory itemInventoryUI;
    public SkillUI skillUI;
    public int curTargetID = 101;

    // 팝업 불러오기
    public UIBase ShowPopup(string popupname, Transform parents = null)
    {
        SoundManager.Instance.ButtonAudio("BasicMenuO_1");
        if (popups.ContainsKey(popupname))
        {
            popups[popupname].gameObject.SetActive(true);
            return null;
        }

        var obj = Resources.Load("Popups/" + popupname, typeof(GameObject)) as GameObject;
        if (!obj)
        {
            Debug.LogWarning($"Failed to ShowPopup({popupname})");
            return null;
        }

        return ShowPopupWithPrefab(obj, popupname, parents);
    }

    public T ShowPopup<T>(Transform parents = null) where T : UIBase
    {
        return ShowPopup(typeof(T).Name, parents) as T;
    }

    public UIBase ShowPopupWithPrefab(GameObject prefab, string popupName, Transform parents = null)
    {
        var obj = Instantiate(prefab, parents);
        obj.name = popupName;
        obj.GetComponent<Canvas>().sortingOrder = popups.Count;
        if (obj.name == "GachaPopUp") obj.GetComponent<Canvas>().sortingOrder = 10;
        return ShowPopup(obj, popupName);
    }

    public UIBase ShowPopup(GameObject obj, string popupname)
    {
        var popup = obj.GetComponent<UIBase>();
        popups.Add(popupname, popup);

        obj.SetActive(true);
        return popup;
    }
    public void BattleShowPopup(GameObject monster, int id)
    {
        if (id != 30)
        {
            var info = Resources.Load("Prefabs/Battle/MonsterInfo") as GameObject;
            if (!info)
            {
                Debug.LogWarning("Failed to MonsterInfo");
            }
            var obj = Instantiate(info, monster.transform.GetChild(1).gameObject.transform);
            int index = int.Parse(obj.transform.parent.parent.name);
            if (!battlePlayerStatus.ContainsKey(index))
            {
                battleEnemyStatus.Add(index, obj);
            }
            var target = Resources.Load("Prefabs/Battle/Target") as GameObject;
            if (!target)
            {
                Debug.LogWarning("Failed to Target");
            }
            Instantiate(target, monster.transform.GetChild(2).gameObject.transform);
        }
        else
        {
            var info = Resources.Load("Prefabs/Battle/BossInfo") as GameObject;
            if (!info)
            {
                Debug.LogWarning("Failed to BossInfo");
            }
            Instantiate(info);
        }
    }
    public void BattlePlayerPopup(int index, Transform statusUI)
    {
        var info = Resources.Load("UI/StatusInfo/Character") as GameObject;
        if (info == null)
        {
            Debug.LogWarning("null");
        }
        var obj = Instantiate(info, statusUI);
        obj.name = index.ToString();
        if (!battlePlayerStatus.ContainsKey(index))
        {
            battlePlayerStatus.Add(index, obj);
        }

    }
    public GameObject PlayerBuffIcon(int index, string IconPath)
    {
        var info = Resources.Load(IconPath) as GameObject;
        if (!info)
        {
            Debug.LogWarning("null");
        }
        var obj = Instantiate(info, battlePlayerStatus[index].gameObject.transform.GetChild(4).gameObject.transform);
        return obj;
    }
    public GameObject EnemyBuffIcon(int index, string IconPath)
    {
        var info = Resources.Load(IconPath) as GameObject;
        if (!info)
        {
            Debug.LogWarning("null");
        }
        var obj = Instantiate(info, battleEnemyStatus[index].gameObject.transform.GetChild(1).gameObject.transform);
        return obj;
    }
    public void BattleEnd()
    {
        popups.Clear();
        battlePlayerStatus.Clear();
        battleEnemyStatus.Clear();
        var _endPopUp = Resources.Load("Prefabs/Battle/ClearUI") as GameObject;
        Instantiate(_endPopUp);
    }
    public void CharacterUpdate()
    {
        foreach (Inventory character in charInventoryUI.Values)
        {
            character.InitCharacterUI();
        }
    }
}

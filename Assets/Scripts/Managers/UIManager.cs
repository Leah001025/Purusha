using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingleTon<UIManager>
{

    private Dictionary<string, UIBase> popups = new Dictionary<string, UIBase>();
    
    public UIBase ShowPopup(string popupname, Transform parents = null)
    {
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
        return ShowPopup(obj, popupName);
    }

    public UIBase ShowPopup(GameObject obj, string popupname)
    {
        var popup = obj.GetComponent<UIBase>();
        popups.Add(popupname, popup);

        obj.SetActive(true);
        return popup;
    }
    public void BattleShowPopup(GameObject monster)
    {
        var info = Resources.Load("Prefabs/Battle/MonsterInfo") as GameObject;
        if (!info)
        {
            Debug.LogWarning("Failed to MonsterInfo");
        }
        Instantiate(info, monster.transform.GetChild(1).gameObject.transform);
        var target = Resources.Load("Prefabs/Battle/Target") as GameObject;
        if (!info)
        {
            Debug.LogWarning("Failed to Target");
        }
        Instantiate(target, monster.transform.GetChild(2).gameObject.transform);
    }
}

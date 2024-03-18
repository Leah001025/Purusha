using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingleTon<UIManager>
{

    private List<UIPopup> popups = new List<UIPopup>();

    // �˾� �ҷ�����
    public UIPopup ShowPopup(string popupname, Transform parents = null)
    {
        var obj = Resources.Load("Popups/" + popupname, typeof(GameObject)) as GameObject;
        if (!obj)
        {
            Debug.LogWarning($"Failed to ShowPopup({popupname})");
            return null;
        }
        return ShowPopupWithPrefab(obj, popupname, parents);
    }

    public T ShowPopup<T>(Transform parents = null) where T : UIPopup
    {
        return ShowPopup(typeof(T).Name,parents) as T;
    }

    public UIPopup ShowPopupWithPrefab(GameObject prefab, string popupName, Transform parents = null)
    {
        var obj = Instantiate(prefab, parents);
        obj.name = popupName;
        return ShowPopup(obj, popupName);
    }

    public UIPopup ShowPopup(GameObject obj, string popupname)
    {
        var popup = obj.GetComponent<UIPopup>();
        popups.Insert(0, popup);

        obj.SetActive(true);
        return popup;
    }

    // �߰��� �޼���: Ư�� �˾� ����
    public void OpenPopup(string popupName)
    {
        ShowPopup(popupName);
    }

    // �߰��� �޼���: �ֻ��� �˾� �ݱ�
    public void ClosePopup()
    {
        if (popups.Count > 0)
        {
            var topPopup = popups[0];
            popups.RemoveAt(0);
            Destroy(topPopup.gameObject);
        }
    }
}

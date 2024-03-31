using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    private Dictionary<string, GameObject> popups = new Dictionary<string, GameObject>();

    private void Start()
    {
        AddPopup("EditTeam");
        AddPopup("UpgradeChr");
    }
    private void AddPopup(string popupName)
    {
        GameObject popupPrefab = Resources.Load<GameObject>("Popups/" + popupName);
        if (popupPrefab != null)
        {
            GameObject popup = Instantiate(popupPrefab);
            popup.SetActive(false);
            popups.Add(popupName, popup);
        }
    }
    public void OpenPopup(string popupName)
    {
        if (popups.ContainsKey(popupName))
        {
            popups[popupName].SetActive(true);
        }
    }

    public void ClosePopup(string popupName)
    {
        if (popups.ContainsKey(popupName))
        {
            popups[popupName].SetActive(false);
        }
    }
}

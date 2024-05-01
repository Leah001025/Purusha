using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private GameObject nameObj;
    public TMP_Text itemName;

    public void ItemName(string name)
    {
        itemName.text = name;
    }
    public void ClickInfo()
    {
        if (nameObj.activeSelf == false)
        {
            nameObj.SetActive(true);
        }
        else 
        {
            nameObj.SetActive(false);
        }
    }
}

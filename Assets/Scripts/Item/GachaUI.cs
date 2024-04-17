using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GachaUI : MonoBehaviour
{
    int curID = 102;
    public TextMeshProUGUI curName;
    public TextMeshProUGUI itemQuantity;
    public Image gachaImage;
    public GameObject gachaObj;
    private Dictionary<int, CharacterData> characters;
    private Dictionary<int, Item> inventory;
    private List<Item> results = new List<Item>(10);
    private void OnEnable()
    {
        characters = GameManager.Instance.User.characters;
        inventory = GameManager.Instance.User.itemInventory;
        itemQuantity.text = $"»Ì±â±Ç {(inventory.ContainsKey(10501) ? inventory[10501].quantity.ToString("0") : "0")}°³";
        var res = Resources.Load<Sprite>("UI/Image/102");
        gachaImage.sprite = res;
    }
    public void GetID(int index)
    {
        curID = index;
        var res = Resources.Load<Sprite>($"UI/Image/{curID}");
        gachaImage.sprite = res;
        curName.text = DataManager.Instance.PlayerDB.GetData(curID).Name;
    }
    public void Gacha1()
    {        
        if (!inventory.ContainsKey(10501)) return;
        results.Clear();
        gachaObj.SetActive(false);
        results.Add(inventory[10501].UseGachaItem(10501, curID));
        var obj = transform.GetChild(2).gameObject;
        obj.gameObject.SetActive(true);
        Button button = obj.transform.GetChild(0).gameObject.GetComponent<Button>();
        button.onClick.AddListener(()=> 
        {
            obj.gameObject.SetActive(false);
            gachaObj.SetActive(true);
        });
        Image icon = obj.transform.GetChild(2).GetChild(0).gameObject.GetComponent<Image>();
        if (results[0].id==10701) icon.sprite = Resources.Load<Sprite>($"UI/Icon/{results[0].value}");
        else icon.sprite = Resources.Load<Sprite>(results[0].spritePath);
        TextMeshProUGUI quantity = obj.transform.GetChild(2).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        quantity.text = results[0].quantity.ToString("0");
        if(UIManager.Instance.itemInventoryUI !=null)
        UIManager.Instance.itemInventoryUI.UpdateInventory();
        UIManager.Instance.CharacterUpdate();
        itemQuantity.text = $"»Ì±â±Ç {(inventory.ContainsKey(10501) ? inventory[10501].quantity.ToString("0") : "0")}°³";
    }
    public void Gacha10()
    {
        if (!inventory.ContainsKey(10501) || inventory[10501].quantity < 10) return;
        results.Clear();
        gachaObj.SetActive(false);
        var obj = transform.GetChild(3).gameObject;
        obj.gameObject.SetActive(true);
        Button button = obj.transform.GetChild(0).gameObject.GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            obj.gameObject.SetActive(false);
            gachaObj.SetActive(true);
        });
        for (int i = 0; i < 10; i++)
        {            
            results.Add(inventory[10501].UseGachaItem(10501, curID));
            Image icon = obj.transform.GetChild(2).GetChild(i).GetChild(0).gameObject.GetComponent<Image>();
            if (results[i].id == 10701) icon.sprite = Resources.Load<Sprite>($"UI/Icon/{results[i].value}");
            else icon.sprite = Resources.Load<Sprite>(results[i].spritePath);
            TextMeshProUGUI quantity = obj.transform.GetChild(2).GetChild(i).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
            quantity.text = results[i].quantity.ToString("0");            
        }
        UIManager.Instance.itemInventoryUI.UpdateInventory();
        UIManager.Instance.CharacterUpdate();
        itemQuantity.text = $"»Ì±â±Ç {(inventory.ContainsKey(10501) ? inventory[10501].quantity.ToString("0") : "0")}°³";
    }
}





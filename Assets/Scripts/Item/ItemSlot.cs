using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public int index;
    public int itemID;
    public Button button;
    private ItemInventory inventory;

    private void Start()
    {
        inventory = transform.parent.parent.parent.gameObject.GetComponent<ItemInventory>();
        button.onClick.AddListener(OnDescription);
    }

    public void OnDescription()
    {
        inventory.description.text = inventory.items[itemID].description;
        inventory.itemName.text = inventory.items[itemID].name;
        inventory.enabledIndex = index;
        inventory.enabledID = itemID;
        inventory.OnOutLine(index);
       
    }
}

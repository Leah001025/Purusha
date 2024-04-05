using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{

    private int _id;
    private string _name;
    private string _description;
    private string _type;
    private float _value;
    private int _quantity;
    private string _spritePath;

    public Item(int id)
    {
        ItemData data = DataManager.Instance.ItemDB.GetData(id);
        _id = id;
        _name = data.Name;
        _description = data.Description;
        _type = data.Type;
        _value = data.Value;
        _quantity = data.Quantity;
        _spritePath = data.SpritePath;
    }
}

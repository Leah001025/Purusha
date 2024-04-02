using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ItemData : DataBase<ItemData>
{
    // ¹«±â Ä¸½¶, Àåºñ Ä¸½¶, °æÇèÄ¡ Ä¸½¶, Ã¼·Â ¹°¾à

    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private string _type;
    [SerializeField] private float _value;
    [SerializeField] private int _quantity;
    [SerializeField] private string _spritePath;

    public int ID { get { return _id; } set {  _id = value; } }
    public string Name { get { return _name; } set { _name = value; } }
    public string Description { get { return _description; } set { _description = value; } }
    public string Type { get { return _type; } set { _type = value; } }
    public float Value { get { return _value; } set { _value = value; } }
    public int Quantity { get { return _quantity; } set { _quantity = value; } }
    public string SpritePath { get { return _spritePath; } set { _spritePath = value; } }
}

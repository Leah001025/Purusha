using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _level;
    [SerializeField] private TMP_Text _atk;
    [SerializeField] private TMP_Text _def;
    [SerializeField] private TMP_Text _health;
    [SerializeField] private TMP_Text _speed;

    private Dictionary<string, int> character = new Dictionary<string, int>()
    {
        { "Breesha", 101 },
        { "Eve", 102 },
        { "Adam", 103 },
        { "Abel", 104 },
        { "Lain", 105 },
        { "Kana", 106 }
    };

    private void Start()
    {
        //gameObject.name = "Eve";
        //ShowCharacterData();
    }


    public void ShowCharacterData()
    {
        string gameObjectName = gameObject.name;

        if (character.ContainsKey(gameObjectName))
        {
            int _id = character[gameObjectName];
            PlayerData playerData = DataManager.Instance.PlayerDB.GetData(_id);

            if (playerData != null)
            {
                _name.text = playerData.Name;
                _level.text = $"Lv. {playerData.Level}";
                _atk.text = $"{playerData.Atk}";
                _def.text = $"{playerData.Def}";
                _health.text = $"{playerData.Health}";
                _speed.text = $"{playerData.Speed}";
            }
        }
    }
}

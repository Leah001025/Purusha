using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Item
{

    public int id;
    public string name;
    public string description;
    public string type;
    public float value;
    public int quantity;
    public string spritePath;
    Dictionary<int, Item> _items;
    Dictionary<int, CharacterData> _characterData;
    UserData _userData;

    public Item(int ID)
    {
        ItemData data = DataManager.Instance.ItemDB.GetData(ID);
        id = ID;
        name = data.Name;
        description = data.Description;
        type = data.Type;
        value = data.Value;
        quantity = data.Quantity;
        spritePath = data.SpritePath;
    }

    public void UseWeaponUpgradeItem(int id)
    {
        _items = GameManager.Instance.User.itemInventory;
        if (_items[id].type == "weaponPotion")
        {
            GameManager.Instance.User.RemoveItem(id);
            return;
        }
        Debug.Log("무기강화 아이템이 아닙니다.");
    }
    public void UseArmorUpgradeItem(int id)
    {
        _items = GameManager.Instance.User.itemInventory;
        if (_items[id].type == "ArmorPotion")
        {
            GameManager.Instance.User.RemoveItem(id);
            return;
        }
        Debug.Log("방어구강화 아이템이 아닙니다.");
    }
    public void UseHealingItem(int id, CharacterData character)
    {
        _items = GameManager.Instance.User.itemInventory;
        if (_items[id].type == "hpPotion")
        {
            character.status.health = Mathf.Min(character.status.health + _items[id].value,character.status.maxhealth);
            GameManager.Instance.User.RemoveItem(id);
            return;
        }
        Debug.Log("회복 아이템이 아닙니다.");
    }
    public void UseExpItem(int id,CharacterData character)
    {
        _items = GameManager.Instance.User.itemInventory;
        if (_items[id].type== "expPotion"&& character.status.exp< 18000)
        {
            character.status.exp += _items[id].value;
            GameManager.Instance.User.RemoveItem(id);
            if (character.status.exp >= DataManager.Instance.LevelDB.GetData(character.status.level + 1).Exp)
                LevelUp(character);
            return;
        }
        Debug.Log("경험치 아이템이 아니거나 만렙입니다.");
    }
    public void UseSkillItem(int id, CharacterData character)
    {
        _items = GameManager.Instance.User.itemInventory;
        if (_items[id].type == "skill")
        {
            string strLevel3 = character.skillData[3].iD.ToString().Substring(5);
            string strLevel4 = character.skillData[4].iD.ToString().Substring(5);
            int level3 = int.Parse(strLevel3);
            int level4 = int.Parse(strLevel4);
            if (level3 == 3&& level4 == 2) return;
            if (level3 == 3)
            {
                int skill4ID = character.skillData[4].iD;
                CharacterSkill skill4UP = new CharacterSkill(skill4ID + 1);
                character.skillData.Remove(4);
                character.skillData.Add(4,skill4UP);
                GameManager.Instance.User.RemoveItem(id);
                return;
            }
            if (level4 == 2)
            {
                int skill3ID = character.skillData[3].iD;
                CharacterSkill skill3UP = new CharacterSkill(skill3ID + 1);
                character.skillData.Remove(3);
                character.skillData.Add(3, skill3UP);
                GameManager.Instance.User.RemoveItem(id);
                return;
            }
            int random = Random.Range(3, 5);
            int skillID = character.skillData[random].iD;
            CharacterSkill skillUP = new CharacterSkill(skillID + 1);
            character.skillData.Remove(random);
            character.skillData.Add(random, skillUP);
            GameManager.Instance.User.RemoveItem(id);
            return;
        }
        Debug.Log("경험치 아이템이 아니거나 만렙입니다.");
    }
    public Item UseGachaItem(int id,int curId)
    {
        string gachaIdstr = "106"+ curId.ToString().Replace("10","0");
        int gachaId = int.Parse(gachaIdstr);
        _items = GameManager.Instance.User.itemInventory;
        _characterData = GameManager.Instance.User.characterDatas;
        _userData = GameManager.Instance.User;
        if (_items[id].type == "etc")
        {
            GameManager.Instance.User.RemoveItem(id);
            float random = Random.Range(0f, 1f);
            if (random < 0.2f)
            {
                if (_characterData.ContainsKey(curId))
                {
                    _userData.AddItem(gachaId);
                    return new Item(gachaId);
                }
                else
                {
                    _userData.AddCharacter(curId);
                    Item result = new Item(10701);
                    result.value = curId;
                    return result;
                }
            }
            else if (random < 0.6f)
            {
                _userData.AddItem(10103,5);
                Item item = new Item(10103);
                item.quantity = 5;
                return item;
            }
            else if (random < 0.8f)
            {
                _userData.AddItem(10201, 2);
                Item item = new Item(10201);
                item.quantity = 2;
                return item;
            }
            else 
            {
                _userData.AddItem(10301, 2);
                Item item = new Item(10301);
                item.quantity = 2;
                return item;
            }
        }
        Debug.Log("가챠 아이템이 아닙니다.");
        return null;
    }
    private void LevelUp(CharacterData character)
    {
        character.status.level++;
        character.status.atk+=10;
        character.status.health += 200;
        character.status.maxhealth += 200;
        character.status.def+=5;
    }
}

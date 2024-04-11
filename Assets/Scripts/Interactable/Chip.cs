using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Chip : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _itemBtn;
    private GameObject _itemUI;
    private UserData _userData;
    private Image _image;
    private TextMeshProUGUI _text;
    private bool isAlreadyGet=false;
    public void Enter()
    {
        _itemBtn.SetActive(true);
    }

    public void Exit()
    {
        _itemBtn.SetActive(false);
    }

    public void Interaction()
    {
        if (isAlreadyGet) return;
        isAlreadyGet = true;
        _userData = GameManager.Instance.User;
        float random = Random.Range(0f, 1f);
        if(random < 0.3f)
        {
            int ran = Random.Range(2, 6);
            int itemID = 10102; //경험치중
            _userData.AddItem(itemID, ran);
            LoadUI(itemID, ran);
        }
        else if(random < 0.6f)
        {
            int ran = Random.Range(2, 6);
            int itemID = 10103; // 경험치대
            _userData.AddItem(itemID, ran);
            LoadUI(itemID, ran);
        }
        else if (random < 0.75f)
        {
            int ran = Random.Range(2, 5);
            int itemID = 10201; //무기강화
            _userData.AddItem(itemID, ran);
            LoadUI(itemID, ran);
        }
        else if (random < 0.9f)
        {
            int ran = Random.Range(2, 5);
            int itemID = 10301; //방어구강화
            _userData.AddItem(itemID, ran);
            LoadUI(itemID, ran);
        }
        else
        {
            int ran = Random.Range(1, 4);
            int itemID = 10501; //뽑기권
            _userData.AddItem(itemID, ran);
            LoadUI(itemID, ran);
        }
    }
    private void LoadUI(int itemID,int quantity)
    {
        var res = ResourceManager.Instance.Load<GameObject>("Popups/ChipItemPopUp");
        _itemUI = Instantiate(res);
        ItemData item = DataManager.Instance.ItemDB.GetData(itemID);
        var sprite = ResourceManager.Instance.Load<Sprite>(item.SpritePath);
        _image = _itemUI.transform.GetChild(0).gameObject.GetComponent<Image>();
        _text = _itemUI.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        _image.sprite = sprite;
        _text.text = $"{item.Name}(을)를 {quantity}개 획득 하였습니다.";
        Destroy(gameObject);
    }

}

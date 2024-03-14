using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPopup : UIBase
{
    public delegate void PopupConfirmFunc();
    private PopupConfirmFunc popupConfirmFunc;

    [SerializeField] private Button btnConfirm;
    [SerializeField] private TMP_Text txtContents;

    // Start is called before the first frame update
    void Start()
    {
        btnConfirm.onClick.AddListener(OnConfirm);
    }

    public void SetPopup(string msg, PopupConfirmFunc popupConfirmFunc)
    {
        txtContents.text = msg;
        this.popupConfirmFunc = popupConfirmFunc;
    }

    void OnConfirm()
    {
        if (popupConfirmFunc != null)
            popupConfirmFunc();
    }
}
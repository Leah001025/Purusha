using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBase : MonoBehaviour
{
    [SerializeField] protected Button closeButton;
    private void Awake()
    {
        closeButton.onClick.AddListener(CloseUI);
    }

    public virtual void CloseUI()
    {
        SoundManager.Instance.ButtonAudio("BasicMenuC_1");
        if(UIManager.Instance.openWorldSceneUI != null) { UIManager.Instance.openWorldSceneUI.UpdateData(); }
        gameObject.SetActive(false);
    }
}

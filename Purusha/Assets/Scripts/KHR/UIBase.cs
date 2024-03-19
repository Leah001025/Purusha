using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBase : MonoBehaviour
{
    [SerializeField] protected Button closeButton;
    private void Awake()
    {
        CloseUI();
    }

    public virtual void CloseUI()
    {
        closeButton.onClick.AddListener(() => gameObject.SetActive(false));
    }
}

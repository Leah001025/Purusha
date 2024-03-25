using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPopUp : UIBase
{
    [SerializeField] private Button skill1;
    [SerializeField] private Button skill2;
    [SerializeField] private Button skill3;
    [SerializeField] private Button skill4;

    private void Awake()
    {
        CloseUI();
    }
    public override void CloseUI()
    {
        skill1.onClick.AddListener(() => gameObject.SetActive(false));
        skill2.onClick.AddListener(() => gameObject.SetActive(false));
        skill3.onClick.AddListener(() => gameObject.SetActive(false));
        skill4.onClick.AddListener(() => gameObject.SetActive(false));
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroPopup : UIPopup
{
    [SerializeField] private Button _startbtn;

    private void Awake()
    {
        _startbtn.onClick.AddListener(() => SceneLoadManager.Instance.ChangeScene("Main"));
        
    }
}

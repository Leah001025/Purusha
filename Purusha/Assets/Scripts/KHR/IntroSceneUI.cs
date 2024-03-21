using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class IntroSceneUI : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Instance.ShowPopup<IntroPopup>();
    }

    
}

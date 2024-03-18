using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneUI : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Instance.ShowPopup<IntroPopup>(transform);
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CloseButtonManager : MonoBehaviour
{
    public PopupManager popupWindow;

    public void OnButtonClick()
    {
        var seq = DOTween.Sequence();
        seq.Play().OnComplete(() => {popupWindow.Hide();});
    }
}

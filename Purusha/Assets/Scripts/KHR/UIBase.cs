using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
   // UI가 활성화 될때 홏

    public virtual void Show()
    {
        gameObject.SetActive(true);
        OnShow();
    }

    //UI가 비활성화 될때
    public virtual void Hide()
    {
        gameObject.SetActive(false);
        OnHide();
    }

    // UI가 활성화 된 이후, 추가적 로직 처리 (동작 커스텀을 위해, 현재는 작동 x )
    protected virtual void OnShow()
    {

    }

    // UI가 비활성화 된 이후, 추가적 로직 처리
    protected virtual void OnHide()
    {

    }

    //UI 초기화를 위한 메소드
    public virtual void Initialize()
    {

    }
}

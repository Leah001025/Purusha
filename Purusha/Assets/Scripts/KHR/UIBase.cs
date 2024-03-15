using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
   // UI�� Ȱ��ȭ �ɶ� �M

    public virtual void Show()
    {
        gameObject.SetActive(true);
        OnShow();
    }

    //UI�� ��Ȱ��ȭ �ɶ�
    public virtual void Hide()
    {
        gameObject.SetActive(false);
        OnHide();
    }

    // UI�� Ȱ��ȭ �� ����, �߰��� ���� ó�� (���� Ŀ������ ����, ����� �۵� x )
    protected virtual void OnShow()
    {

    }

    // UI�� ��Ȱ��ȭ �� ����, �߰��� ���� ó��
    protected virtual void OnHide()
    {

    }

    //UI �ʱ�ȭ�� ���� �޼ҵ�
    public virtual void Initialize()
    {

    }
}

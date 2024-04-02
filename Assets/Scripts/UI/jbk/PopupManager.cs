using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PopupManager : MonoBehaviour
{
    private static PopupManager _instance;
    public static PopupManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        DOTween.Init();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        var seq = DOTween.Sequence();
        seq.Append(transform.DOScale(1.1f, 0.2f));
        seq.Append(transform.DOScale(1f, 0.1f));
        seq.Play();
    }

    public void Hide()
    {
        var seq = DOTween.Sequence();
        seq.Play().OnComplete(() => {gameObject.SetActive(false);});
    }
    public void CloseBtn()
    {
        SceneLoadManager.Instance.LoadingChangeScene("Main");
    }
}

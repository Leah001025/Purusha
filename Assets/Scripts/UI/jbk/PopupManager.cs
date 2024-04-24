using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

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
        SoundManager.Instance.ButtonAudio("BasicMenuO_1");
        gameObject.SetActive(true);
        var seq = DOTween.Sequence();
        seq.Append(transform.DOScale(1.1f, 0.2f));
        seq.Append(transform.DOScale(1f, 0.1f));
        seq.Play();
    }

    public void Hide()
    {
        SoundManager.Instance.ButtonAudio("BasicMenuC_1");
        var seq = DOTween.Sequence();
        seq.Play().OnComplete(() => { gameObject.SetActive(false); });
    }

    public void CloseBtn()
    {
        SceneLoadManager.Instance.LoadingChangeScene("MainScene");
    }
   
}

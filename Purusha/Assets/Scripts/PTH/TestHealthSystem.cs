using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class TestHealthSystem : MonoBehaviour
{
    public event Action OnHit;

    private int characterType;

    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        characterType = Utility.GetLayerWithTag(gameObject);

    }
    public void TakeDamage()
    {
        switch ((int)characterType)
        {
            case (int)CharacterType.Player:
                //OnHit?.Invoke();
                break;
            case (int)CharacterType.Monster:
                OnHit?.Invoke();
                break;
        }
    }
}

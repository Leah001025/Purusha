using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnIndicator : MonoBehaviour
{
    string characterID;
    Vector2 chracterTurnPos;

    private void Awake()
    {
        characterID = gameObject.name;
        chracterTurnPos = Vector2.zero;
    }
    private void Update()
    {
        chracterTurnPos.y = BattleManager.Instance.GetTurnIndicator(characterID);
        gameObject.GetComponent<RectTransform>().anchoredPosition = chracterTurnPos;
    }
}

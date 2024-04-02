using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnIndicator : MonoBehaviour
{
    string characterID;
    Vector2 chracterTurnPos;
    private Dictionary<int,UnitInfo> lUnitInfo = new Dictionary<int, UnitInfo>();

    private void Awake()
    {
        
    }
    private void Start()
    {
        lUnitInfo = BattleManager.Instance.lUnitInfo;
    }
    private void Update()
    {
        chracterTurnPos.y = BattleManager.Instance.GetTurnIndicator(characterID);
        gameObject.GetComponent<RectTransform>().anchoredPosition = chracterTurnPos;
    }
    private void Init()
    {
        for (int i = 1; i <= BattleManager.Instance.lUnitInfo.Count; i++)
        {
            var res = Resources.Load<GameObject>("UI/TurnIndigator/Character");
            var obj = Instantiate<GameObject>(res,gameObject.transform.GetChild(0));

            var portrait = Resources.Load<Sprite>("UI/Icon/Potrait_" + lUnitInfo[i].unitID.ToString());
        }
    }
}

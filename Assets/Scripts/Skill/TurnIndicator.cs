using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TurnIndicator : MonoBehaviour
{
    private Dictionary<int, GameObject> characters = new Dictionary<int, GameObject>();
    private Dictionary<int, TextMeshProUGUI> unitGauges = new Dictionary<int, TextMeshProUGUI>();
    private Dictionary<int, Outline> outLines = new Dictionary<int, Outline>();
    private Dictionary<int, UnitInfo> lUnitInfo;
    private int targetIndex;
    private int lastTargetIndex;
    private int lastCount;

    private void Start()
    {
        lUnitInfo = BattleManager.Instance.lUnitInfo;
        Init();
        lastCount = lUnitInfo.Count;
    }
    private void FixedUpdate()
    {
        SortTeam();
        SetGauge();
        SetTarget();
    }
    private void Init()
    {
        for (int i = 1; i <= BattleManager.Instance.lUnitInfo.Count; i++)
        {
            var res = Resources.Load<GameObject>("UI/TurnIndigator/Character");
            var obj = Instantiate<GameObject>(res, gameObject.transform.GetChild(1));
            Outline outline = obj.GetComponent<Outline>();
            Image portrait = obj.transform.GetChild(0).gameObject.GetComponent<Image>();
            TextMeshProUGUI gauge = obj.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
            unitGauges.Add(i, gauge);
            portrait.sprite = Resources.Load<Sprite>("UI/Icon/Portrait_" + lUnitInfo[i].unitID.ToString());
            gauge.text = lUnitInfo[i].unitGauge.ToString("0");
            characters.Add(i, obj);
            outLines.Add(i, outline);
        }
    }
    private int SetIndex(int key)
    {
        int count = 0;
        foreach (UnitInfo unitInfo in lUnitInfo.Values)
        {
            if (lUnitInfo.ContainsKey(key) && lUnitInfo[key].unitGauge < unitInfo.unitGauge) count++;
        }
        return count;
    }
    private void SetGauge()
    {
        for (int i = 1; i <= lastCount; i++)
        {
            if (!lUnitInfo.ContainsKey(i))
            {
                RemoveCharacter(i);
            }
            else
                unitGauges[i].text = lUnitInfo[i].unitGauge>=100?"100": lUnitInfo[i].unitGauge.ToString("0");
        }
    }
    private void SortTeam()
    {
        foreach (KeyValuePair<int, GameObject> character in characters)
        {
            character.Value.transform.SetSiblingIndex(SetIndex(character.Key));
        }
    }
    private void SetTarget()
    {
        if (BattleManager.Instance.target != null)
            targetIndex = int.Parse(BattleManager.Instance.target.name);
        if (lastTargetIndex != targetIndex)
        {
            foreach (KeyValuePair<int, Outline> outLine in outLines)
            {
                if (outLine.Key == targetIndex)
                {
                    outLine.Value.enabled = true;
                }
                else
                {
                    outLine.Value.enabled = false;
                }
            }
            lastTargetIndex = targetIndex;
        }

    }
    private void RemoveCharacter(int key)
    {
        if (characters.ContainsKey(key))
        {
            unitGauges.Remove(key);
            outLines.Remove(key);
            Destroy(characters[key]);
            characters.Remove(key);
        }
    }
}

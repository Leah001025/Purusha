using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageInfoUI : MonoBehaviour
{
    [SerializeField] private TMP_Text CharacterName;
    [SerializeField] private TMP_Text AttackDamage;
    [SerializeField] private TMP_Text ReceivedDamage;
    [SerializeField] private TMP_Text Support;

    private void Start()
    {
        CharacterName.text = GameManager.Instance.User.teamData[int.Parse(gameObject.name)].status.name;
        AttackDamage.text = BattleManager.Instance.battleInfo.characterInfo[int.Parse(gameObject.name)].attackDamages.ToString();
        ReceivedDamage.text = BattleManager.Instance.battleInfo.characterInfo[int.Parse(gameObject.name)].receivedDamages.ToString();
        Support.text = BattleManager.Instance.battleInfo.characterInfo[int.Parse(gameObject.name)].support.ToString();
    }
}

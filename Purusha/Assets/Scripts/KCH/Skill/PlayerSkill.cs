using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] public SkillDataBase skillDB;
    public SkillData skillData;
    void Start()
    {
        skillDB = DataManager.Instance.SkillDB;
        skillData = skillDB.GetData(101011);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

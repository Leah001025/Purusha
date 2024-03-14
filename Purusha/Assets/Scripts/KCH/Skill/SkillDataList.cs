using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDataList : DataBaseList<int, SkillData>
{
    public List<SkillData> skillData;

    public override void SetData(List<SkillData> skillDatas)
    {
        datas = new Dictionary<int, SkillData>(skillDatas.Count);

        skillDatas.ForEach(obj =>
        {
            SkillData item = new SkillData();
            item.SetData(obj);
            datas.Add(item.ID, item);
        });
    }
}

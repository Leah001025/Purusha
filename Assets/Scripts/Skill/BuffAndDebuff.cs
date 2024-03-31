using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAndDebuff : MonoBehaviour
{
    CharacterData buffData;
    //private int attackUp;
    //private int attackDown;
    //private int defUp;
    //private int defDown;
    //private int buffID;
    private float shield;
    public BuffAndDebuff(int id)
    {
        buffData = (CharacterData)GameManager.Instance.User.characterDatas[id].CloneCharacter(id);        
    }
    //public float SetBuff(int id)
    //{
    //    switch (id)
    //    {
    //        case 101:
    //            shield = buffData.status.def * 1f;
    //            break;
    //        case 102:
    //            break;
    //    }

    //}
}

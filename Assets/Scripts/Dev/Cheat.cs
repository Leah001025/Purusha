using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    public void Weapon()
    {
        GameManager.Instance.User.AddItem(10201, 10);
    }
    public void Armor()
    {
        GameManager.Instance.User.AddItem(10301, 10);
    }
    public void Exp()
    {
        GameManager.Instance.User.AddItem(10103, 10);
    }
    public void Gacha()
    {
        GameManager.Instance.User.AddItem(10501, 10);
    }
    public void Heal()
    {
        GameManager.Instance.User.AddItem(10402, 10);
    }
}

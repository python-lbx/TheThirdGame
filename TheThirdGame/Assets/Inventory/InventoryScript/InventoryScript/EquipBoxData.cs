using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "" , menuName = "Inventory/New EquipBoxData")]
public class EquipBoxData : ScriptableObject
{   
    [Header("部位屬性")]
    public string whichPart;
    public int HP;
    public int ATK;
    public int DEF;
    public int Speed;
    public bool Isequip;

    public void Reset() 
    {
        HP = 0;
        ATK = 0;
        DEF = 0;
        Speed = 0;
        Isequip = false;
    }
}

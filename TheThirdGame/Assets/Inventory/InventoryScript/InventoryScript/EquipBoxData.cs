using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "" , menuName = "Inventory/New EquipBoxData")]
public class EquipBoxData : ScriptableObject
{   
    //裝備欄list
    public List<Item> EquipList = new List<Item>();
    public int[] E_hp;
    public int[] E_atk;
    public int[] E_def;
    public int[] E_speed;
}

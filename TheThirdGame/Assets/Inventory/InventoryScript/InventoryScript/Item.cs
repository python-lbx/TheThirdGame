using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "" , menuName =("Inventory/New Item"))]
public class Item : ScriptableObject
{
    [Header("物件訊息")]
    public string ItemName;
    public Sprite ItemImage;
    //public int ItemHeld;
    [TextArea]
    public string ItemInfo;
    public bool equipable;
    
    [Header("物件屬性")]
    public int HP; //血量
    public int ATK; //攻擊力
    public int CRI; //爆擊率
    public int CSD; //爆擊傷害
    public int SPD; //移動速度
    public float DropRate;
}

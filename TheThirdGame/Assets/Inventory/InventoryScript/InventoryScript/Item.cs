using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "" , menuName =("Inventory/New Item"))]
public class Item : ScriptableObject
{
    [Header("物件訊息")]
    public string ItemName;
    public Sprite ItemImage;
    public int ItemHeld;
    [TextArea]
    public string ItemInfo;
    public bool equipable;
    
    [Header("物件屬性")]
    public int HP;
    public int ATK;
    public int DFS;
    public int Speed;
    public float DropRate;
}

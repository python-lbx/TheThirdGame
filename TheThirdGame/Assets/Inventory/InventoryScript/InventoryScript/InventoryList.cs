using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "" , menuName = "Inventory/New Inventory List")]
public class InventoryList : ScriptableObject
{
    public List<Item> ItemList = new List<Item>();
}

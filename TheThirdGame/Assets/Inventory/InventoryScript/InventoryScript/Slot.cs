using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int slotID;
    public Image slotImage;
    public Text slotNum;
    public string slotInfo;
    public string slotName;
    public int thisHP;
    public int thisATK;
    public int thisDEF;
    public int thisSpeed;

    public GameObject itemInSlot;

    private void Awake() 
    {

    }

    public void ItemOnClicked()
    {
        InventoryManager.updateItemInfo(slotInfo);
    }

    public void SetUpSlot(Item item)
    {
        if(item == null)
        {
            itemInSlot.SetActive(false);
            return;
        }

        slotName = item.ItemName;
        slotImage.sprite = item.ItemImage;
        //slotNum.text = item.ItemHeld.ToString();
        slotInfo = item.ItemInfo;
    }

    public void setupData(int hp)
    {
        thisHP = hp;
    }
}

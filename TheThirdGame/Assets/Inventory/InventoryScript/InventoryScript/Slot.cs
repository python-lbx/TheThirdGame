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
    public EquipmentMenu equip;
    public InventoryList mybag;

    public GameObject itemInSlot;

    private void Awake() 
    {
        equip = FindObjectOfType<EquipmentMenu>();
    }

    public void ItemOnClicked()
    {
        //InventoryManager.updateItemInfo(slotInfo);
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

    public void setupData(int hp,int atk,int def,int speed)
    {
        thisHP = hp;
        thisATK = atk;
        thisDEF = def;
        thisSpeed = speed;
    }

    public void Equip()
    {
        if(slotName == "Clothes")
        {
            if(!equip.clothes.Isequip) //沒有裝備
            {   
                //裝上裝備
                equip.clothes.Isequip = true; //裝上
                equip.clothes_Image.color = Color.white; //顯示
                equip.clothes.HP = thisHP; //裝備欄數據更新
                equip.clothes.ATK = thisATK;
                equip.clothes.DEF = thisDEF;
                equip.clothes.Speed = thisSpeed;

                //背包數據更新 
                //數值歸0
                ResetMybagItemList();
            }
            else //有裝備 更換裝備 
            {
                //裝備欄數據暫存
                var temp_HP = equip.clothes.HP; 
                var temp_ATK = equip.clothes.ATK;
                var temp_DEF = equip.clothes.DEF;
                var temp_SPEED = equip.clothes.Speed;

                //更換新裝備欄數據 裝上新裝備
                equip.clothes.HP = thisHP; 
                equip.clothes.ATK = thisATK;
                equip.clothes.DEF = thisDEF;
                equip.clothes.Speed = thisSpeed;

                //原裝備欄裝備放回背包 暫存數據回傳  
                MybagDateEqualTemp(temp_HP,temp_ATK,temp_DEF,temp_SPEED);
            }
        }
        else if(slotName == "Sword")
        {
            if(!equip.sword.Isequip)
            {
                equip.sword.Isequip = true;
                equip.sword_Image.color = Color.white;
                equip.sword.HP = thisHP;
                equip.sword.ATK = thisATK;
                equip.sword.DEF = thisDEF;
                equip.sword.Speed = thisSpeed;

                ResetMybagItemList();
            }
            else
            {
                var temp_HP = equip.sword.HP;
                var temp_ATK = equip.sword.ATK;
                var temp_DEF = equip.sword.DEF;
                var temp_SPEED = equip.sword.Speed;

                equip.sword.HP = thisHP;
                equip.sword.ATK = thisATK;
                equip.sword.DEF = thisDEF;
                equip.sword.Speed = thisSpeed;

                MybagDateEqualTemp(temp_HP,temp_ATK,temp_DEF,temp_SPEED);
            }
        }

        InventoryManager.RefreshItem(); //所有動作最後都要刷新背包

    }

    public void ResetMybagItemList()
    {
        mybag.ItemList[slotID]  = null; 
        mybag.hp[slotID] = 0;
        mybag.atk[slotID] = 0;
        mybag.def[slotID] = 0;
        mybag.speed[slotID] = 0;
    }

    public void MybagDateEqualTemp(int hp,int atk, int def, int speed)
    {
        mybag.hp[slotID] = hp; 
        mybag.atk[slotID] = atk;
        mybag.def[slotID] = def;
        mybag.speed[slotID] = speed;
    }
}

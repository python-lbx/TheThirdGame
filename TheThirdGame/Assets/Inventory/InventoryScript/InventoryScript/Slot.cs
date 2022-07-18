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
    public int thisCRI;
    public int thisCSD;
    public int thisSPD;
    public EquipmentMenu equip;
    public InventoryList mybag;

    public GameObject itemInSlot;


    private void OnEnable() 
    {
        equip = FindObjectOfType<EquipmentMenu>();
    }
    private void Awake() 
    {
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

    public void setupData(int hp,int atk,int cri,int csd,int spd)
    {
        thisHP = hp;
        thisATK = atk;
        thisCRI = cri;
        thisCSD = csd;
        thisSPD = spd;
    }

    public void Equip() //裝裝備
    {   
        if(slotName == "Head")
        {
            if(!equip.head.Isequip)
            {
                equip.head.Isequip = true;
                equip.head_Image.color = Color.white;
                equip.head.HP = thisHP;
                equip.head.ATK = thisATK;
                equip.head.CRI = thisCRI;
                equip.head.CSD = thisCSD;
                equip.head.SPD = thisSPD;

                ResetMybagItemList();
            }
            else
            {
                var temp_HP = equip.head.HP;
                var temp_ATK = equip.head.ATK;
                var temp_CRI = equip.head.CRI;
                var temp_CSD = equip.head.CSD;
                var temp_SPD = equip.head.SPD;

                equip.head.HP = thisHP;
                equip.head.ATK = thisATK;
                equip.head.CRI = thisCRI;
                equip.head.CSD = thisCSD;
                equip.head.SPD = thisSPD;

                MybagDateEqualTemp(temp_HP,temp_ATK,temp_CRI,temp_CSD,temp_SPD);
            }
        }

        if(slotName == "Sword")
        {
            if(!equip.sword.Isequip)
            {
                equip.sword.Isequip = true;
                equip.sword_Image.color = Color.white;
                equip.sword.HP = thisHP;
                equip.sword.ATK = thisATK;
                equip.sword.CRI = thisCRI;
                equip.sword.CSD = thisCSD;
                equip.sword.SPD = thisSPD;

                ResetMybagItemList();
            }
            else
            {
                var temp_HP = equip.sword.HP;
                var temp_ATK = equip.sword.ATK;
                var temp_CRI = equip.sword.CRI;
                var temp_CSD = equip.sword.CSD;
                var temp_SPD = equip.sword.SPD;

                equip.sword.HP = thisHP;
                equip.sword.ATK = thisATK;
                equip.sword.CRI = thisCRI;
                equip.sword.CSD = thisCSD;
                equip.sword.SPD = thisSPD;

                MybagDateEqualTemp(temp_HP,temp_ATK,temp_CRI,temp_CSD,temp_SPD);
            }
        }

        if(slotName == "Clothes")
        {
            if(!equip.clothes.Isequip) //沒有裝備
            {   
                //裝上裝備
                equip.clothes.Isequip = true; //裝上
                equip.clothes_Image.color = Color.white; //顯示
                equip.clothes.HP = thisHP; //裝備欄數據更新
                equip.clothes.ATK = thisATK;
                equip.clothes.CRI = thisCRI;
                equip.clothes.CSD = thisCSD;
                equip.clothes.SPD = thisSPD;

                //背包數據更新 
                //數值歸0
                ResetMybagItemList();
            }
            else //有裝備 更換裝備 
            {
                //裝備欄數據暫存
                var temp_HP = equip.clothes.HP; 
                var temp_ATK = equip.clothes.ATK;
                var temp_CRI = equip.clothes.CRI;
                var temp_CSD = equip.clothes.CSD;
                var temp_SPD = equip.clothes.SPD;

                //更換新裝備欄數據 裝上新裝備
                equip.clothes.HP = thisHP; 
                equip.clothes.ATK = thisATK;
                equip.clothes.CRI = thisCRI;
                equip.clothes.CSD = thisCSD;
                equip.clothes.SPD = thisSPD;

                //原裝備欄裝備放回背包 暫存數據回傳  
                MybagDateEqualTemp(temp_HP,temp_ATK,temp_CRI,temp_CSD,temp_SPD);
            }
        }

        if(slotName == "Pants")
        {
            if(!equip.pants.Isequip)
            {
                equip.pants.Isequip = true;
                equip.Pants_Image.color = Color.white;
                equip.pants.HP = thisHP;
                equip.pants.ATK = thisATK;
                equip.pants.CRI = thisCRI;
                equip.pants.CSD = thisCSD;
                equip.pants.SPD = thisSPD;

                ResetMybagItemList();
            }
            else
            {
                var temp_HP = equip.pants.HP;
                var temp_ATK = equip.pants.ATK;
                var temp_CRI = equip.pants.CRI;
                var temp_CSD = equip.pants.CSD;
                var temp_SPD = equip.pants.SPD;

                equip.pants.HP = thisHP;
                equip.pants.ATK = thisATK;
                equip.pants.CRI = thisCRI;
                equip.pants.CSD = thisCSD;
                equip.pants.SPD = thisSPD;

                MybagDateEqualTemp(temp_HP,temp_ATK,temp_CRI,temp_CSD,temp_SPD);
            }
        }

        if(slotName == "Shoe")
        {
            if(!equip.shoe.Isequip)
            {
                equip.shoe.Isequip = true;
                equip.Shoe_Image.color = Color.white;
                equip.shoe.HP = thisHP;
                equip.shoe.ATK = thisATK;
                equip.shoe.CRI = thisCRI;
                equip.shoe.CSD = thisCSD;
                equip.shoe.SPD = thisSPD;

                ResetMybagItemList();
            }
            else
            {
                var temp_HP = equip.shoe.HP;
                var temp_ATK = equip.shoe.ATK;
                var temp_CRI = equip.shoe.CRI;
                var temp_CSD = equip.shoe.CSD;
                var temp_SPD = equip.shoe.SPD;

                equip.shoe.HP = thisHP;
                equip.shoe.ATK = thisATK;
                equip.shoe.CRI = thisCRI;
                equip.shoe.CSD = thisCSD;
                equip.shoe.SPD = thisSPD;

                MybagDateEqualTemp(temp_HP,temp_ATK,temp_CRI,temp_CSD,temp_SPD);
            }
        }


        InventoryManager.RefreshItem(); //所有動作最後都要刷新背包

    }

    public void ResetMybagItemList()
    {
        mybag.ItemList[slotID]  = null; 
        mybag.hp[slotID] = 0;
        mybag.atk[slotID] = 0;
        mybag.cri[slotID] = 0;
        mybag.csd[slotID] = 0;
        mybag.spd[slotID] = 0;
    }

    public void MybagDateEqualTemp(int hp ,int atk ,int cri ,int csd ,int spd)
    {
        mybag.hp[slotID] = hp; 
        mybag.atk[slotID] = atk;
        mybag.cri[slotID] = cri;
        mybag.csd[slotID] = csd;
        mybag.spd[slotID] = spd;
    }

}

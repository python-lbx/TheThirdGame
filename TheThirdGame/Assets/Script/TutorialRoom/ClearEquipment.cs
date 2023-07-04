using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearEquipment : MonoBehaviour
{
    [Header("身上的裝備數據")]
    public EquipBoxData head;
    public EquipBoxData sword;
    public EquipBoxData clothes;
    public EquipBoxData pants;
    public EquipBoxData shoe;

    [Header("身上的裝備圖示")]
    public Image head_Image;
    public Image sword_Image;
    public Image clothes_Image;
    public Image Pants_Image;
    public Image Shoe_Image;

    [Header("背包")]
    public InventoryList myBag;
    InventoryManager inventory;

    public void ClearEquip()
    {
        if(head & sword & clothes & pants & shoe != null)
        {
            head.Reset();
            head_Image.color = Color.black;

            sword.Reset();
            sword_Image.color = Color.black;

            clothes.Reset();
            clothes_Image.color = Color.black;

            pants.Reset();
            Pants_Image.color = Color.black;

            shoe.Reset();
            Shoe_Image.color = Color.black;
        }

        for(int i = 0; i < myBag.ItemList.Count ; i++)
        {
            myBag.ItemList[i] = null;
            myBag.hp[i] = 0;
            myBag.atk[i] = 0;
            myBag.cri[i] = 0;
            myBag.csd[i] = 0;
            myBag.spd[i] = 0;
        }

        InventoryManager.RefreshItem();
    }
}

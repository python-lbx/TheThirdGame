using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentMenu : MonoBehaviour,IPointerEnterHandler
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

    [Header("裝備數據")]
    public Item Head_Item;
    public Item Sword_Item;
    public Item Clothes_Item;
    public Item Pants_Item;
    public Item Shoe_Item;

    [Header("玩家數據")]
    public CharacterData player;
    [Header("數據庫")]
    public InventoryManager inventory;
    public InventoryList playerInventory;

    [Header("裝備按鈕")]
    public GameObject HeadButton;
    public GameObject SwordButton;
    public GameObject ClothesButton;
    public GameObject PantsButton;
    public GameObject ShoeButton;
    public Part_Attribute PA;


    void Start()
    {
        if(head & sword & clothes & pants & shoe != null)
        {
            head.Reset();
            sword.Reset();
            clothes.Reset();
            pants.Reset();
            shoe.Reset();
        }

        inventory = FindObjectOfType<InventoryManager>();

    }

    void Update()
    {

    }

    public void ButtonCheck(string PartName)
    {
        //找到部位
        
        print(PartName);
        switch(PartName)
        {         //名字要對到
            case "Head":
            PA = HeadButton.GetComponent<Part_Attribute>();
            break;

            case "Sword":
            PA = SwordButton.GetComponent<Part_Attribute>();
            break;

            case "Clothes":
            PA = ClothesButton.GetComponent<Part_Attribute>();
            break;

            case "Pants":
            PA = PantsButton.GetComponent<Part_Attribute>();
            break;

            case "Shoe":
            PA = ShoeButton.GetComponent<Part_Attribute>();
            break;
        }

    }
    public void TakeOffTheGear() //脫裝備
    {   
        //檢測背包是否還有空位
        if(FindObjectOfType<InventoryManager>().isfull)
        {
            FindObjectOfType<InventoryManager>().itemInfo.text = "背包滿了";
        }

        if(PA.PartName == "Head" && head.Isequip)
        {
            for(int i = 0; i<playerInventory.ItemList.Count ; i++)
            {
                if(playerInventory.ItemList[i] == null)
                {
                    playerInventory.ItemList[i] = Head_Item;
                    playerInventory.hp[i] = head.HP;
                    playerInventory.atk[i] = head.ATK;
                    playerInventory.def[i] = head.DEF;
                    playerInventory.speed[i] = head.Speed;
                    head_Image.color = Color.black;
                    head.Reset();
                    break;
                }
            }
        }

        if(PA.PartName == "Sword" && sword.Isequip)
        {
            for(int i = 0; i < playerInventory.ItemList.Count ; i++)
            {
                if(playerInventory.ItemList[i] == null)
                {
                    playerInventory.ItemList[i] = Sword_Item;
                    playerInventory.hp[i] = sword.HP;
                    playerInventory.atk[i] = sword.ATK;
                    playerInventory.def[i] = sword.DEF;
                    playerInventory.speed[i] = sword.Speed;
                    sword_Image.color = Color.black;
                    sword.Reset();
                    break;
                }
            }
        }

        if(PA.PartName == "Clothes" && clothes.Isequip)
        {
            for(int i = 0; i < playerInventory.ItemList.Count ; i++)
            {
                if(playerInventory.ItemList[i] == null)
                {
                    playerInventory.ItemList[i] = Clothes_Item;
                    playerInventory.hp[i] = clothes.HP;
                    playerInventory.atk[i] = clothes.ATK;
                    playerInventory.def[i] = clothes.DEF;
                    playerInventory.speed[i] = clothes.Speed;
                    clothes_Image.color = Color.black;
                    clothes.Reset(); //脫下重置部位屬性
                    break;
                }
            }
        }

        if(PA.PartName == "Pants" && pants.Isequip)
        {
            for(int i = 0; i < playerInventory.ItemList.Count ; i++)
            {
                if(playerInventory.ItemList[i] == null)
                {
                    playerInventory.ItemList[i] = Pants_Item;
                    playerInventory.hp[i] = pants.HP;
                    playerInventory.atk[i] = pants.ATK;
                    playerInventory.def[i] = pants.DEF;
                    playerInventory.speed[i] = pants.Speed;
                    Pants_Image.color = Color.black;
                    pants.Reset();
                    break;
                }
            }
        }

        if(PA.PartName == "Shoe" && shoe.Isequip)
        {
            for(int i = 0; i < playerInventory.ItemList.Count ; i++)
            {
                if(playerInventory.ItemList[i] == null)
                {
                    playerInventory.ItemList[i] = Shoe_Item;
                    playerInventory.hp[i] = shoe.HP;
                    playerInventory.atk[i] = shoe.ATK;
                    playerInventory.def[i] = shoe.DEF;
                    playerInventory.speed[i] = shoe.Speed;
                    Shoe_Image.color = Color.black;
                    shoe.Reset();
                    break;
                }
            }
        }
        
        InventoryManager.RefreshItem();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    //顯示屬性
    public void OnPointerEnter(PointerEventData eventData) 
    {
        var item = eventData.pointerCurrentRaycast.gameObject.GetComponent<Part_Attribute>();

        if(eventData.pointerCurrentRaycast.gameObject.name == "Head")
        {
            showPartInfo(item);
        }

        if(eventData.pointerCurrentRaycast.gameObject.name == "Sword")
        {
            showPartInfo(item);
        }

        if(eventData.pointerCurrentRaycast.gameObject.name == "Clothes")
        {
            showPartInfo(item);
        }

        if(eventData.pointerCurrentRaycast.gameObject.name == "Pants")
        {
            showPartInfo(item);
        }

        if(eventData.pointerCurrentRaycast.gameObject.name == "Shoe")
        {
            showPartInfo(item);
        }

        //print(item.PartHP);
    
    }

    public void showPartInfo(Part_Attribute item)
    {
        inventory.itemInfo.text = "GEAR: "  + item.PartName + 
                                  " HP: "    + item.PartHP +
                                  " ATK: "   + item.PartATK +
                                  " DEF: "    + item.PartDEF + 
                                  " SPEED: "  + item.PartSPEED;      
    }

    /*
    public void tempvoid()
    {
        for(int i = 0; i < playerInventory.ItemList.Count ; i++)
        {
            if(playerInventory.ItemList[i] == null)
            {
                playerInventory.ItemList[i] = Clothes_Item;
                playerInventory.hp[i] = clothes.HP;
                clothes_Image.color = Color.black;
                clothes.Reset();
                clothes = null;
                break;
            }
        }
    }
    */
}

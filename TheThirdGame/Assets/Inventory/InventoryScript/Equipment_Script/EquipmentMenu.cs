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
    public Item Clothes_Item;
    public Item Sword_Item;
    //public EquipBoxData thishead,thisweapon,thisclothes,thispants,thisshoe;
    //public bool ehead,eweapon,eclothes,epants,eshoe;
    [Header("玩家數據")]
    public CharacterData player;
    [Header("數據庫")]
    public InventoryManager inventory;
    public InventoryList playerInventory;


    void Start()
    {
        if(head & sword & clothes & pants & shoe != null)
        {
            head.Reset();
            sword.Reset();
            clothes.Reset();
            pants.Reset();
            shoe.Reset();

            /*thishead = Instantiate(head);
            thisweapon = Instantiate(weapon);
            thisclothes = Instantiate(clothes);
            thispants = Instantiate(pants);
            thisshoe = Instantiate(shoe);*/
        }

        inventory = FindObjectOfType<InventoryManager>();

    }
    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {


    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }



    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

    }
    public void TakeOffTheGear() //脫裝備
    {   
        //找到部位屬性
        var part =GetComponentInChildren<Part_Attribute>();
        print(part.PartName + part.PartHP);

        //檢測背包是否還有空位
        if(FindObjectOfType<InventoryManager>().isfull)
        {
            FindObjectOfType<InventoryManager>().itemInfo.text = "背包滿了";
        }

        if(part.PartName == "Clothes")
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
        else if(part.PartName == "Sword")
        {
            for(int i = 0; i < playerInventory.ItemList.Count ; i++)
            {
                if(playerInventory.ItemList[i] == null)
                {
                    playerInventory.ItemList[i] = Sword_Item;
                    playerInventory.hp[i] = sword.HP;
                    playerInventory.atk[i] = sword.ATK;
                    

                }
            }
        }


        InventoryManager.RefreshItem();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if(eventData.pointerCurrentRaycast.gameObject.name == "Clothes")
        {
        var item = eventData.pointerCurrentRaycast.gameObject.GetComponent<Part_Attribute>();
        inventory.itemInfo.text ="EQUIP: "  + item.PartName + 
                                 " HP: "    + item.PartHP +
                                 " ATK: "   + item.PartATK +
                                 " DEF: "    + item.PartDEF + 
                                 " SPEED: "  + item.PartSPEED;           
        }
        //print(item.PartHP);
    
    }

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerEnterHandler,IPointerExitHandler
{
    public Transform originalParent;
    public InventoryList mybag;
    public GameObject player;
    public bool rightClickOn;

    [Header("ItemInfo")]
    public int currentItemID;
    public string Itemname; //for生成
    public int ItemHp;
    public int ItemAtk;
    public int ItemDef;
    public int ItemSpeed;
    

    
    InventoryManager inventory;
    int ItemofWhichNo; //生成裝備

    private void Start() 
    {   
        inventory = FindObjectOfType<InventoryManager>();
        player = GameObject.Find("Player");
    }

    private void Update() 
    {
        if(Input.GetMouseButtonDown(1))
        {
            rightClickOn = true;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent; //原父組件
        //Itemname = originalParent.GetComponent<Slot>().slotName; //原物件名
        //currentItemID = originalParent.GetComponent<Slot>().slotID; //原物件ID

        //記錄資料
        original_Data( originalParent.GetComponent<Slot>().slotID,
                       originalParent.GetComponent<Slot>().slotName,
                       originalParent.GetComponent<Slot>().thisHP,
                       originalParent.GetComponent<Slot>().thisATK,
                       originalParent.GetComponent<Slot>().thisDEF,
                       originalParent.GetComponent<Slot>().thisSpeed );


        //脫離
        transform.SetParent(transform.parent.parent);
        //在鼠標上
        transform.position = eventData.position;


        //print(Itemname);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //在鼠標上
        transform.position = eventData.position;
        //print(Itemname);   

        //找尋 後續
        for(int i = 0;i<inventory.objPrefab.Length;i++)
        {
            if(inventory.objPrefab[i].name == Itemname)
            {
                ItemofWhichNo = i;
                //print(ItemofWhichNo);
                //print(i);
                break;
            }
        }

        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.transform.parent.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    {   
        var obj = eventData.pointerCurrentRaycast.gameObject;

        Vector2 newpos = new Vector2(player.transform.position.x + 0.5f,player.transform.position.y);

            if(obj.name == "ItemImage" && obj != null)
            {   
                //放新位置
                transform.SetParent(obj.transform.parent.parent);
                transform.position = obj.transform.position;

                //暫存Data step 1
                var temp_ID = mybag.ItemList[currentItemID]; 
                var temp_hp = mybag.hp[currentItemID];
                var temp_atk = mybag.atk[currentItemID];
                var temp_def = mybag.def[currentItemID];
                var temp_speed = mybag.speed[currentItemID];

                //step 2 將物件由 0位放到1位 將ID 0 改為ID 1
                mybag.ItemList[currentItemID] = mybag.ItemList[obj.GetComponentInParent<Slot>().slotID];
                mybag.hp[currentItemID] = mybag.hp[obj.GetComponentInParent<Slot>().slotID];
                mybag.atk[currentItemID] = mybag.atk[obj.GetComponentInParent<Slot>().slotID];
                mybag.def[currentItemID] = mybag.def[obj.GetComponentInParent<Slot>().slotID];
                mybag.speed[currentItemID] = mybag.speed[obj.GetComponentInParent<Slot>().slotID];

                //step 3 將物件 由1位放到0位 將ID改為暫存data
                mybag.ItemList[obj.GetComponentInParent<Slot>().slotID] = temp_ID;
                mybag.hp[obj.GetComponentInParent<Slot>().slotID] = temp_hp;
                mybag.atk[obj.GetComponentInParent<Slot>().slotID] = temp_atk;
                mybag.def[obj.GetComponentInParent<Slot>().slotID] = temp_def;
                mybag.speed[obj.GetComponentInParent<Slot>().slotID] = temp_speed;


                //換位
                obj.transform.parent.position = originalParent.position;
                obj.transform.parent.SetParent(originalParent);
                GetComponent<CanvasGroup>().blocksRaycasts = true;

                InventoryManager.RefreshItem();
                return;
            }

            else if(obj.name == "Slot(Clone)" && obj != null)
            {
                transform.SetParent(obj.transform);
                transform.position = obj.transform.position;

                //鼠標底下物件數值更新
                mybag.ItemList[obj.GetComponentInParent<Slot>().slotID] = mybag.ItemList[currentItemID];
                mybag.hp[obj.GetComponentInParent<Slot>().slotID] = mybag.hp[currentItemID];
                mybag.atk[obj.GetComponentInParent<Slot>().slotID] = mybag.atk[currentItemID];
                mybag.def[obj.GetComponentInParent<Slot>().slotID] = mybag.def[currentItemID];
                mybag.speed[obj.GetComponentInParent<Slot>().slotID] = mybag.speed[currentItemID];

                if(obj.GetComponent<Slot>().slotID != currentItemID)
                {
                    mybag.ItemList[currentItemID] = null;
                    mybag.hp[currentItemID] = 0;
                    mybag.atk[currentItemID] = 0;
                    mybag.def[currentItemID] = 0;
                    mybag.speed[currentItemID] = 0;
                }

                GetComponent<CanvasGroup>().blocksRaycasts = true;

                InventoryManager.RefreshItem();
                return;
            }

            else if(obj.name == "BagBG")
            {
                //生成舊道具 且不會屬性隨機
                inventory.objPrefab[ItemofWhichNo].GetComponent<ItemOnWorld>().tempDate.HP = mybag.hp[currentItemID];
                inventory.objPrefab[ItemofWhichNo].GetComponent<ItemOnWorld>().tempDate.ATK = mybag.atk[currentItemID];
                inventory.objPrefab[ItemofWhichNo].GetComponent<ItemOnWorld>().tempDate.DEF = mybag.def[currentItemID];
                inventory.objPrefab[ItemofWhichNo].GetComponent<ItemOnWorld>().tempDate.Speed = mybag.speed[currentItemID];

                Instantiate(inventory.objPrefab[ItemofWhichNo],newpos,Quaternion.identity);

                inventory.itemInfo.text = "";

                //清空數據
                mybag.ItemList[currentItemID] = null;
                mybag.hp[currentItemID] = 0;
                mybag.atk[currentItemID] = 0;
                mybag.def[currentItemID] = 0;
                mybag.speed[currentItemID] = 0;

                GetComponent<CanvasGroup>().blocksRaycasts = true;
                InventoryManager.RefreshItem();
                return;
            }
            else
            {
                transform.SetParent(originalParent);
                transform.position = originalParent.position;
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                InventoryManager.RefreshItem();
            }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var item = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>();
        inventory.itemInfo.text ="EQUIP: "  + item.slotName + 
                                 " HP: "    + item.thisHP +
                                 " ATK: "   + item.thisATK +
                                 " DEF: "    + item.thisDEF + 
                                 " SPEED: "  + item.thisSpeed;
        
        //print("我選中了"+item.slotName+"HP:"+item.thisHP);
        //print(eventData.pointerCurrentRaycast.gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //inventory.ItemInfoScreen.SetActive(false);

        //print("我離開了"+eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void original_Data(int id,string name,int hp,int atk,int def,int speed) //顯示點擊物件屬性
    {
        currentItemID = id;
        Itemname = name;
        ItemHp = hp;
        ItemAtk = atk;
        ItemDef = def;
        ItemSpeed = speed;

        print("ID" +currentItemID + "name"+Itemname +"HP"+ ItemHp +"ATK"+ ItemAtk +"DEF"+ ItemDef +"SPEED"+ ItemSpeed);
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerEnterHandler,IPointerExitHandler
{
    public Transform originalParent;
    public InventoryList mybag;
    public GameObject player;

    [Header("ItemInfo")]
    public int currentItemID;
    public string Itemname; //for生成
    public int ItemHp;
    public int ItemAtk;
    public int ItemCRI;
    public int ItemCSD;
    public int ItemSPD;
    

    
    InventoryManager inventory;
    int ItemofWhichNo; //生成裝備

    private void Start() 
    {   
        inventory = FindObjectOfType<InventoryManager>();
        player = GameObject.Find("Player");
    }

    private void Update() 
    {

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
                       originalParent.GetComponent<Slot>().thisCRI,
                       originalParent.GetComponent<Slot>().thisCSD,
                       originalParent.GetComponent<Slot>().thisSPD );


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
                var temp_cri = mybag.cri[currentItemID];
                var temp_csd = mybag.csd[currentItemID];
                var temp_spd = mybag.spd[currentItemID];

                //step 2 將物件由 0位放到1位 將ID 0 改為ID 1
                mybag.ItemList[currentItemID] = mybag.ItemList[obj.GetComponentInParent<Slot>().slotID];
                mybag.hp[currentItemID] = mybag.hp[obj.GetComponentInParent<Slot>().slotID];
                mybag.atk[currentItemID] = mybag.atk[obj.GetComponentInParent<Slot>().slotID];
                mybag.cri[currentItemID] = mybag.cri[obj.GetComponentInParent<Slot>().slotID];
                mybag.csd[currentItemID] = mybag.csd[obj.GetComponentInParent<Slot>().slotID];
                mybag.spd[currentItemID] = mybag.spd[obj.GetComponentInParent<Slot>().slotID];

                //step 3 將物件 由1位放到0位 將ID改為暫存data
                mybag.ItemList[obj.GetComponentInParent<Slot>().slotID] = temp_ID;
                mybag.hp[obj.GetComponentInParent<Slot>().slotID] = temp_hp;
                mybag.atk[obj.GetComponentInParent<Slot>().slotID] = temp_atk;
                mybag.cri[obj.GetComponentInParent<Slot>().slotID] = temp_cri;
                mybag.csd[obj.GetComponentInParent<Slot>().slotID] = temp_csd;
                mybag.spd[obj.GetComponentInParent<Slot>().slotID] = temp_spd;


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
                mybag.cri[obj.GetComponentInParent<Slot>().slotID] = mybag.cri[currentItemID];
                mybag.csd[obj.GetComponentInParent<Slot>().slotID] = mybag.csd[currentItemID];
                mybag.spd[obj.GetComponentInParent<Slot>().slotID] = mybag.spd[currentItemID];

                if(obj.GetComponent<Slot>().slotID != currentItemID)
                {
                    mybag.ItemList[currentItemID] = null;
                    mybag.hp[currentItemID] = 0;
                    mybag.atk[currentItemID] = 0;
                    mybag.cri[currentItemID] = 0;
                    mybag.csd[currentItemID] = 0;
                    mybag.spd[currentItemID] = 0;
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
                inventory.objPrefab[ItemofWhichNo].GetComponent<ItemOnWorld>().tempDate.CRI = mybag.cri[currentItemID];
                inventory.objPrefab[ItemofWhichNo].GetComponent<ItemOnWorld>().tempDate.CSD = mybag.csd[currentItemID];
                inventory.objPrefab[ItemofWhichNo].GetComponent<ItemOnWorld>().tempDate.SPD = mybag.spd[currentItemID];

                Instantiate(inventory.objPrefab[ItemofWhichNo],newpos,Quaternion.identity);

                inventory.itemInfo.text = "";

                //清空數據
                mybag.ItemList[currentItemID] = null;
                mybag.hp[currentItemID] = 0;
                mybag.atk[currentItemID] = 0;
                mybag.cri[currentItemID] = 0;
                mybag.csd[currentItemID] = 0;
                mybag.spd[currentItemID] = 0;

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
                                 " CRI: "    + item.thisCRI + 
                                 " CSD: "    + item.thisCSD + 
                                 " SPD: "  + item.thisSPD;
        
        //print("我選中了"+item.slotName+"HP:"+item.thisHP);
        //print(eventData.pointerCurrentRaycast.gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //inventory.ItemInfoScreen.SetActive(false);

        //print("我離開了"+eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void original_Data(int id,string name,int hp,int atk,int cri,int csd,int spd) //顯示點擊物件屬性
    {
        currentItemID = id;
        Itemname = name;
        ItemHp = hp;
        ItemAtk = atk;
        ItemCRI= cri;
        ItemCSD = csd;
        ItemSPD = spd;

        print("ID" +currentItemID + "name"+Itemname +"HP"+ ItemHp +"ATK"+ ItemAtk +"CRI"+ ItemCRI +"CSD"+ ItemCSD +"SPD"+ ItemSPD);
        
    }
}

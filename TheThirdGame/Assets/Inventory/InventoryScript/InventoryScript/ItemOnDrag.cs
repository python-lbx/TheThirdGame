using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Transform originalParent;
    public InventoryList mybag;
    public string Itemname;
    public int currentItemID;
    public GameObject player;

    InventoryManager inventory;
    int ItemofWhichNo;

    private void Start() 
    {   
        inventory = FindObjectOfType<InventoryManager>();
        player = GameObject.Find("Player");
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        Itemname = originalParent.GetComponent<Slot>().slotName;
        currentItemID = originalParent.GetComponent<Slot>().slotID;
        transform.SetParent(transform.parent.parent);
        transform.position = eventData.position;


        //print(Itemname);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        print(Itemname);   

        for(int i = 0;i<inventory.objPrefab.Length;i++)
        {
            if(inventory.objPrefab[i].name == Itemname)
            {
                ItemofWhichNo = i;
                print(ItemofWhichNo);
                print(i);
            }
        }

        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    {   
        var obj = eventData.pointerCurrentRaycast.gameObject;
        Vector2 newpos = new Vector2(player.transform.position.x + 2.5f,player.transform.position.y);

            if(obj.name == "ItemImage" && obj != null)
            {
                transform.SetParent(obj.transform.parent.parent);
                transform.position = obj.transform.position;
                var temp = mybag.ItemList[currentItemID];
                mybag.ItemList[currentItemID] = mybag.ItemList[obj.GetComponentInParent<Slot>().slotID];
                mybag.ItemList[obj.GetComponentInParent<Slot>().slotID] = temp;

                obj.transform.parent.position = originalParent.position;
                obj.transform.parent.SetParent(originalParent);
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }

            else if(obj.name == "Slot(Clone)" && obj != null)
            {
                transform.SetParent(obj.transform);
                transform.position = obj.transform.position;

                mybag.ItemList[obj.GetComponentInParent<Slot>().slotID] = mybag.ItemList[currentItemID];

                if(obj.GetComponent<Slot>().slotID != currentItemID)
                {
                    mybag.ItemList[currentItemID] = null;
                }

                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }

            else if(obj.name == "BagBG")
            {
                mybag.ItemList[currentItemID] = null;

                Instantiate(inventory.objPrefab[ItemofWhichNo],newpos,Quaternion.identity);
                inventory.itemInfo.text = "";

                GetComponent<CanvasGroup>().blocksRaycasts = true;
                InventoryManager.RefreshItem();
                return;
            }
            else
            {
                transform.SetParent(originalParent);
                transform.position = originalParent.position;
                GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
        
        
    }
}

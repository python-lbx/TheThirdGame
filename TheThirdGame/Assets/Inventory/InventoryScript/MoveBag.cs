using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MoveBag : MonoBehaviour,IDragHandler
{
    public RectTransform currentRect;
    public InventoryManager inventory;
    public GameObject MyBag;
    public bool OpenMyBag;
    
    public void OnDrag(PointerEventData eventData)
    {
        currentRect.anchoredPosition += eventData.delta;
    }

    private void Update() 
    {   
        MyBag.SetActive(OpenMyBag);

        if(Input.GetKeyDown(GameManager.GM.bag))
        {
            InventoryManager.RefreshItem();
            OpenMyBag = !OpenMyBag;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(MyBag.activeSelf)
            {
                OpenMyBag = false;
            }
        }
    }



}

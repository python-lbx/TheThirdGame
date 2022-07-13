using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public Item thisItem;
    public Item tempDate;
    public InventoryList playerInventory;
    public CharacterStats Player;

    //public float droprate;

    

    bool here;

    private void OnEnable() 
    {
        if(tempDate != null)
        {
            thisItem = Instantiate(tempDate);
        }
    }
    private void Awake() 
    {
        //droprate = thisItem.DropRate;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
        
    }
    // Start is called before the first frame update
    private void Update() 
    {
        if(here && Input.GetKeyDown(KeyCode.Q))
        {
            Player.Character.AttackPower += thisItem.ATK;
            Player.Character.Defense += thisItem.DFS;
            Player.Character.Speed += thisItem.Speed;
            Player.Character.MaxHP += thisItem.HP;
            print("你獲得了:"    + thisItem.ItemName+
                  "最大生命值:"  + Player.Character.MaxHP +
                  "攻擊力:" + Player.Character.AttackPower +
                  "防御力:" + Player.Character.Defense +
                  "移動速度:" + Player.Character.Speed);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //print(other.gameObject.name);
            here = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //print(other.gameObject.name);
            here = false;
        }    
    }

    public void AddNewItem()
    {
        //避免重覆
        /*if(!playerInventory.ItemList.Contains(thisItem)){}*/ 
        
        for(int i = 0 ; i < playerInventory.ItemList.Count ; i++)
        {
            if(playerInventory.ItemList[i] == null)
            {
                playerInventory.ItemList[i] = thisItem;
                Destroy(this.gameObject);
                break;
            }
        }

        InventoryManager.RefreshItem();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public Item thisItem; //副本
    public Item tempDate; //正本
    public InventoryList playerInventory;
    //public CharacterStats Player;
    public Transform point;

    public bool IsNewItem;

    public int ohp;
    public int oatk;
    public int odef;
    public int ospeed;

    float rate;
    //public LayerMask player;
    //bool here;

    private void OnEnable() 
    {
        if(tempDate != null)
        {
            thisItem = Instantiate(tempDate);
        }
        
        if(IsNewItem) //新裝備 隨機屬性
        {
            RandomAttributes();
        }
        else
        {
            thisItem.HP = ohp;
            thisItem.ATK = oatk;
            thisItem.DEF = odef;
            thisItem.Speed = ospeed;
        }
    }
    private void Awake() 
    {
        //droprate = thisItem.DropRate;
        //Player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
        
    }
    // Start is called before the first frame update
    private void Update() 
    {
        #region 廢案
        //here =  Physics2D.OverlapBox(transform.position,transform.localScale,0,player);

        /*if(FindObjectOfType<test>().here && Input.GetKeyDown(KeyCode.Q))
        {
            Player.Character.AttackPower += thisItem.ATK;
            Player.Character.Defense += thisItem.DEF;
            Player.Character.Speed += thisItem.Speed;
            Player.Character.MaxHP += thisItem.HP;
            print("你獲得了:"    + thisItem.ItemName+
                  "最大生命值:"  + Player.Character.MaxHP +
                  "攻擊力:" + Player.Character.AttackPower +
                  "防御力:" + Player.Character.Defense +
                  "移動速度:" + Player.Character.Speed);
            Destroy(this.gameObject);
            
            //FindObjectOfType<test>().EquipInfo.SetActive(false);
        }

        /*if(here)
        {
            FindObjectOfType<test>()._Equip = this;
            FindObjectOfType<test>().EquipInfo.SetActive(here);
            FindObjectOfType<test>().EquipInfo.transform.position = point.transform.position;
        }
        */
        #endregion

    }

    public void RandomAttributes()
    {
        //隨機屬性
        rate = Random.value;

        //print(rate);
        switch (thisItem.ItemName)
        {
            case "Clothes":
            if(rate < 0.05f)
            {
            thisItem.HP = Random.Range(8,10);
            thisItem.Speed = -Random.Range(8,10);
            }
            else if(rate < 0.3f)
            {
            thisItem.HP = Random.Range(5,8);
            thisItem.Speed = -Random.Range(5,8);
            }
            else
            {   
            thisItem.HP = Random.Range(0,5);
            thisItem.Speed = -Random.Range(1,5);
            }
            break;

            case "Pants":
            if(rate < 0.05f)
            {
            thisItem.HP = Random.Range(8,10);
            thisItem.Speed = -Random.Range(8,10);
            }
            else if(rate < 0.3f)
            {
            thisItem.HP = Random.Range(5,8);
            thisItem.Speed = -Random.Range(5,8);
            }
            else
            {   
            thisItem.HP = Random.Range(0,5);
            thisItem.Speed = -Random.Range(1,5);
            }
            break;

            case "Sword":
            if(rate < 0.05f)
            {
            thisItem.ATK = Random.Range(8,10);
            thisItem.Speed = Random.Range(4,5);
            }
            else if(rate < 0.3f)
            {
            thisItem.ATK = Random.Range(5,8);
            thisItem.Speed = Random.Range(3,4);
            }
            else
            {   
            thisItem.ATK = Random.Range(0,5);
            thisItem.Speed = Random.Range(0,3);
            }
            break;

            case "Shoe":
            if(rate < 0.05f)
            {
            thisItem.HP = Random.Range(8,10);
            thisItem.Speed = Random.Range(8,10);
            }
            else if(rate < 0.3f)
            {
            thisItem.HP = Random.Range(5,8);
            thisItem.Speed = Random.Range(5,8);
            }
            else
            {   
            thisItem.HP = Random.Range(0,5);
            thisItem.Speed = Random.Range(1,5);
            }
            break;
        }

        ohp = thisItem.HP;
        oatk = thisItem.ATK;
        odef = thisItem.DEF;
        ospeed = thisItem.Speed;
    }

    #region  廢案
    /*private void OnDrawGizmos() 
    {
        Gizmos.color = here? Color.red : Color.clear;
        Gizmos.DrawCube(transform.position,transform.localScale);
    }*/
    #endregion

    #region  廢案
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
    #endregion
}

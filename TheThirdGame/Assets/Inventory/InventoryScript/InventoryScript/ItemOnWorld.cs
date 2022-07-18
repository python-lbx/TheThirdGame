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

    [Header("裝備面板")]
    public bool IsNewItem;
    public int hp;
    public int atk;
    public int cri;
    public int csd;
    public int spd;

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
        //數值再調
        switch (thisItem.ItemName)
        {   
            //魔力?暴擊?負重?
            case "Head":
            if(rate < 0.05f)
            {
            thisItem.CRI = Random.Range(10,15);
            thisItem.CSD = Random.Range(4,5);
            }
            else if(rate < 0.3f)
            {
            thisItem.CRI = Random.Range(5,10);
            thisItem.CSD = Random.Range(3,4);
            }
            else
            {   
            thisItem.CRI = Random.Range(0,4);
            thisItem.CSD = Random.Range(1,2);
            }
            break;
            
            //攻擊?暴擊?速度?
            case "Sword":
            if(rate < 0.05f)
            {
            thisItem.ATK = Random.Range(8,10);
            thisItem.CRI = Random.Range(4,5);
            }
            else if(rate < 0.3f)
            {
            thisItem.ATK = Random.Range(5,8);
            thisItem.CRI = Random.Range(3,4);
            }
            else
            {   
            thisItem.ATK = Random.Range(0,5);
            thisItem.CRI = Random.Range(0,3);
            }
            break;

            //血量?負重?
            case "Clothes":
            if(rate < 0.05f)
            {
            thisItem.HP = Random.Range(6,10);
            thisItem.SPD = -Random.Range(3,5);
            }
            else if(rate < 0.3f)
            {
            thisItem.HP = Random.Range(3,6);
            thisItem.SPD = -Random.Range(2,3);
            }
            else
            {   
            thisItem.HP = Random.Range(0,3);
            thisItem.SPD = -Random.Range(1,2);
            }
            break;

            //血量?負重?
            case "Pants":
            if(rate < 0.05f)
            {
            thisItem.HP = Random.Range(8,10);
            thisItem.SPD = -Random.Range(3,5);
            }
            else if(rate < 0.3f)
            {
            thisItem.HP = Random.Range(4,7);
            thisItem.SPD = -Random.Range(2,3);
            }
            else
            {   
            thisItem.HP = Random.Range(1,3);
            thisItem.SPD = -Random.Range(1,2);
            }
            break;

            //血量?速度?暴擊?
            case "Shoe":
            if(rate < 0.05f)
            {
            thisItem.HP = Random.Range(8,10);
            thisItem.SPD = Random.Range(8,10);
            }
            else if(rate < 0.3f)
            {
            thisItem.HP = Random.Range(5,8);
            thisItem.SPD = Random.Range(5,8);
            }
            else
            {   
            thisItem.HP = Random.Range(0,5);
            thisItem.SPD = Random.Range(1,5);
            }
            break;
        }

        hp = thisItem.HP;
        atk = thisItem.ATK;
        cri = thisItem.CRI;
        csd = thisItem.CSD;
        spd = thisItem.SPD;
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

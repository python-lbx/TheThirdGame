using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PickUpAndCheckItemInfo : MonoBehaviour
{
    [Header("裝備道具")]
    public GameObject EquipInfo;
    public ItemOnWorld _Equip;
    public Text _EquipName;
    public Text _EquipHP;
    public Text _EquipATK;
    public Text _EquipDEF;
    public Text _EquipSpeed;
    public bool here;
    public LayerMask item;
    public CharacterStats Player;
    public InventoryList playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        EquipInfo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //偵測
        Collider2D _here = Physics2D.OverlapBox(transform.position,transform.localScale,0,item);
        
        //bool
        here = _here;

        if(_here != null)
        {
            //print(_here.gameObject.name);
            EquipInfo.SetActive(true); 
            _Equip = _here.gameObject.GetComponent<ItemOnWorld>();
            EquipInfo.transform.position = _Equip.point.transform.position;
            //print(_Equip.thisItem.HP);
        }
        else
        {
            EquipInfo.SetActive(false);
        }   

        //顯示視窗與數據
        if(_Equip != null)
        {
            _EquipName.text = _Equip.thisItem.ItemName;
            _EquipHP.text = _Equip.thisItem.HP.ToString();
            _EquipATK.text = _Equip.thisItem.ATK.ToString();
            _EquipDEF.text = _Equip.thisItem.DEF.ToString();
            _EquipSpeed.text = _Equip.thisItem.Speed.ToString();
        }

        //拾取物件且強化
        if(here && Input.GetKeyDown(KeyCode.Z))
        {
            if(FindObjectOfType<InventoryManager>().isfull)
            {
                FindObjectOfType<InventoryManager>().itemInfo.text = "背包滿了";
            }
            
            for(int i = 0 ; i < playerInventory.ItemList.Count ; i++)
            {
                if(playerInventory.ItemList[i] == null)
                {
                    //print(_Equip.thisItem);
                    //顯示物品在背包
                    playerInventory.ItemList[i] = _Equip.tempDate;
                    //array數據記錄
                    playerInventory.hp[i] = _Equip.thisItem.HP;
                    playerInventory.atk[i] = _Equip.thisItem.ATK;
                    playerInventory.def[i] = _Equip.thisItem.DEF;
                    playerInventory.speed[i] = _Equip.thisItem.Speed;

                    Destroy(_here.gameObject);
                    break;
                }
            }

            InventoryManager.RefreshItem();
            //showstate();
        }
    }

    public void AddNewItem()
    {
        //避免重覆
        /*if(!playerInventory.ItemList.Contains(thisItem)){}*/ 
    }

    public void showstate()
    {
            Player.Character.AttackPower += _Equip.thisItem.ATK;
            Player.Character.Defense += _Equip.thisItem.DEF;
            Player.Character.Speed += _Equip.thisItem.Speed;
            Player.Character.MaxHP += _Equip.thisItem.HP;
            print("你獲得了:"    + _Equip.thisItem.ItemName+
                  "最大生命值:"  + Player.Character.MaxHP +
                  "攻擊力:" + Player.Character.AttackPower +
                  "防御力:" + Player.Character.Defense +
                  "移動速度:" + Player.Character.Speed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Attributes : MonoBehaviour
{
    [Header("屬性面板")]
    public Text HP;
    public Text ATK;
    public Text DEF;
    public Text Speed;

    [Header("部位數據")]
    public EquipBoxData Head_Item;
    public EquipBoxData Sword_Item;
    public EquipBoxData Clothes_Item;
    public EquipBoxData Pants_Item;
    public EquipBoxData Shoe_Item;

    [Header("頭部屬性")]
    public int head_hp;
    public int head_atk;
    public int head_def;
    public int head_speed;
    [Header("劍屬性")]
    public int sword_hp;
    public int sword_atk;
    public int sword_def;
    public int sword_speed;
    [Header("胸部屬性")]
    public int clothes_hp;
    public int clothes_atk;
    public int clothes_def;
    public int clothes_speed;
    [Header("腿部屬性")]
    public int pants_hp;
    public int pants_atk;
    public int pants_def;
    public int pants_speed;
    [Header("腳部屬性")]
    public int shoe_hp;
    public int shoe_atk;
    public int shoe_def;
    public int shoe_speed;

    [Header("組件")]
    public EquipmentMenu MyEquip;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ShowPartAtt()
    {
        head_hp = Head_Item.HP;
        head_atk = Head_Item.ATK;
        head_def = Head_Item.DEF;
        head_speed = Head_Item.Speed;

        sword_hp = Sword_Item.HP;
        sword_atk = Sword_Item.ATK;
        sword_def = Sword_Item.DEF;
        sword_speed = Sword_Item.Speed;
    }
}

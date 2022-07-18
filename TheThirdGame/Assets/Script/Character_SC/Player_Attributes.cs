using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Attributes : MonoBehaviour
{
    [Header("屬性面板")]
    public Text HP;
    public Text ATK;
    public Text CRI;
    public Text CSD;
    public Text SPD;

    [Header("部位數據")]
    public EquipBoxData Head_Item;
    public EquipBoxData Sword_Item;
    public EquipBoxData Clothes_Item;
    public EquipBoxData Pants_Item;
    public EquipBoxData Shoe_Item;

    [Header("頭部屬性")]
    public int head_hp;
    public int head_atk;
    public int head_cri;
    public int head_csd;
    public int head_spd;
    [Header("劍屬性")]
    public int sword_hp;
    public int sword_atk;
    public int sword_cri;
    public int sword_csd;
    public int sword_spd;
    [Header("胸部屬性")]
    public int clothes_hp;
    public int clothes_atk;
    public int clothes_cri;
    public int clothes_csd;
    public int clothes_spd;
    [Header("腿部屬性")]
    public int pants_hp;
    public int pants_atk;
    public int pants_cri;
    public int pants_csd;
    public int pants_spd;
    [Header("腳部屬性")]
    public int shoe_hp;
    public int shoe_atk;
    public int shoe_cri;
    public int shoe_csd;
    public int shoe_spd;

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
        head_cri = Head_Item.CRI;
        head_csd = Head_Item.CSD;
        head_spd = Head_Item.SPD;

        sword_hp = Sword_Item.HP;
        sword_atk = Sword_Item.ATK;
        sword_cri = Sword_Item.CRI;
        sword_csd = Sword_Item.CSD;
        sword_spd = Sword_Item.SPD;
    }
}

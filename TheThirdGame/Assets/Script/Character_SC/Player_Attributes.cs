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

    [Header("HP")]
    public int HP_Before;
    public int HP_After;
    public int HP_Compare;
    public int Player_HP;
    [Header("ATK")]
    public int ATK_Before;
    public int ATK_After;
    public int ATK_Compare;
    public int Player_ATK;
    [Header("CRI")]
    public int CRI_Before;
    public int CRI_Ater;
    public int CRI_Compare;
    public int Player_CRI;
    [Header("CSD")]
    public int CSD_Before;
    public int CSD_After;
    public int CSD_Compare;
    public int Player_CSD;
    [Header("PSD")]
    public int SPD_Before;
    public int SPD_After;
    public int SPD_Compare;
    public int Player_SPD;


    // Start is called before the first frame update
    private void Awake() 
    {
        Head_Item.Reset();
        Sword_Item.Reset();
        Clothes_Item.Reset();
        Pants_Item.Reset();
        Shoe_Item.Reset();
    }
    void Start()
    {
        //之前
        HP_Before = Head_Item.HP + Sword_Item.HP + Clothes_Item.HP + Pants_Item.HP + Shoe_Item.HP;

        ATK_Before = Head_Item.ATK + Sword_Item.ATK + Clothes_Item.ATK + Pants_Item.ATK + Shoe_Item.ATK;

        CRI_Before = Head_Item.CRI + Sword_Item.CRI + Clothes_Item.CRI + Pants_Item.CRI + Shoe_Item.CRI;

        CSD_Before = Head_Item.CSD + Sword_Item.CSD + Clothes_Item.CSD + Pants_Item.CSD + Shoe_Item.CSD;

        SPD_Before = Head_Item.SPD + Sword_Item.SPD + Clothes_Item.SPD + Pants_Item.SPD + Shoe_Item.SPD;
    }

    // Update is called once per frame
    void Update()
    {
        ShowPartAtt();
        updateAtt();
        //之後
        // HP_After = Head_Item.HP + Sword_Item.HP + Clothes_Item.HP + Pants_Item.HP + Shoe_Item.HP;
        
        // ATK_After = Head_Item.ATK + Sword_Item.ATK + Clothes_Item.ATK + Pants_Item.ATK + Shoe_Item.ATK;

        // CRI_Ater = Head_Item.CRI + Sword_Item.CRI + Clothes_Item.CRI + Pants_Item.CRI + Shoe_Item.CRI;

        // CSD_After = Head_Item.CSD + Sword_Item.CSD + Clothes_Item.CSD + Pants_Item.CSD + Shoe_Item.CSD;

        // SPD_After = Head_Item.SPD + Sword_Item.SPD + Clothes_Item.SPD + Pants_Item.SPD + Shoe_Item.SPD;

        // //比較
        // HP_Compare = HP_After - HP_Before;

        // ATK_Compare = ATK_After - ATK_Before;

        // CRI_Compare = CRI_Ater - CRI_Before;

        // CSD_Compare = CSD_After - CSD_Before;

        // SPD_Compare = SPD_After - SPD_Before;

        // //重載
        // HP_Before = HP_After;

        // ATK_Before = ATK_After;

        // CRI_Before = CRI_Ater;

        // CSD_Before = CSD_After;

        // SPD_Before = SPD_After;

        // //輸出
        // HP.text = (Player_HP += HP_Compare).ToString();

        // ATK.text = (Player_ATK += ATK_Compare).ToString();

        // CRI.text = (Player_CRI += CRI_Compare).ToString();

        // CSD.text = (Player_CSD += CSD_Compare).ToString();

        // SPD.text = (Player_SPD += SPD_Compare).ToString();

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

        clothes_hp = Clothes_Item.HP;
        clothes_atk = Clothes_Item.ATK;
        clothes_cri = Clothes_Item.CRI;
        clothes_csd = Clothes_Item.CSD;
        clothes_spd = Clothes_Item.SPD;

        pants_hp = Pants_Item.HP;
        pants_atk = Pants_Item.ATK;
        pants_cri = Pants_Item.CRI;
        pants_csd = Pants_Item.CSD;
        pants_spd = Pants_Item.SPD;

        shoe_hp = Shoe_Item.HP;
        shoe_atk = Shoe_Item.ATK;
        shoe_cri = Shoe_Item.CRI;
        shoe_csd = Shoe_Item.CSD;
        shoe_spd = Shoe_Item.SPD;
    }

    public void updateAtt()
    {
        HP_After = Head_Item.HP + Sword_Item.HP + Clothes_Item.HP + Pants_Item.HP + Shoe_Item.HP;
        
        ATK_After = Head_Item.ATK + Sword_Item.ATK + Clothes_Item.ATK + Pants_Item.ATK + Shoe_Item.ATK;

        CRI_Ater = Head_Item.CRI + Sword_Item.CRI + Clothes_Item.CRI + Pants_Item.CRI + Shoe_Item.CRI;

        CSD_After = Head_Item.CSD + Sword_Item.CSD + Clothes_Item.CSD + Pants_Item.CSD + Shoe_Item.CSD;

        SPD_After = Head_Item.SPD + Sword_Item.SPD + Clothes_Item.SPD + Pants_Item.SPD + Shoe_Item.SPD;

        //比較
        HP_Compare = HP_After - HP_Before;

        ATK_Compare = ATK_After - ATK_Before;

        CRI_Compare = CRI_Ater - CRI_Before;

        CSD_Compare = CSD_After - CSD_Before;

        SPD_Compare = SPD_After - SPD_Before;

        //重載
        HP_Before = HP_After;

        ATK_Before = ATK_After;

        CRI_Before = CRI_Ater;

        CSD_Before = CSD_After;

        SPD_Before = SPD_After;

        //輸出
        HP.text = (Player_HP += HP_Compare).ToString();

        ATK.text = (Player_ATK += ATK_Compare).ToString();

        CRI.text = (Player_CRI += CRI_Compare).ToString();

        CSD.text = (Player_CSD += CSD_Compare).ToString();

        SPD.text = (Player_SPD += SPD_Compare).ToString();
    }
}

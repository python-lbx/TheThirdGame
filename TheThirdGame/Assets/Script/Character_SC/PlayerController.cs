using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("角色屬性")]
    public float HP;
    public float CurrentHP;
    public float ATK;
    public float CRI;
    public float CSD;
    //public bool isCrit;
    //public float rate;
    //[Header("面板傷害")]
    //public int damage;
    //public float CRI_Damage;

    /*[Header("UI插件")]
    public Text CR_Text;
    public Text CDR_Text;*/

    //[Header("目標物件")]
    //public GameObject targetpos;
    public Player_Attributes CharacterState;
    // Start is called before the first frame update
    void Start()
    {
        HP = CharacterState.Player_HP;
        CurrentHP = HP;
    }

    // Update is called once per frame
    void Update()
    {

        //傷害先進行計算
        //damage = CharacterState.Player_ATK;
        HP = CharacterState.Player_HP;
        ATK = CharacterState.Player_ATK;
        CRI = CharacterState.Player_CRI;
        CSD = CharacterState.Player_CSD;
        //CRI_Damage = Mathf.Round(damage * (CSD/100)) ;
    
    }

    public void GetDamage(float damage)
    {
        CurrentHP -= damage;
    }
}

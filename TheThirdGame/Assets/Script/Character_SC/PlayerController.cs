using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("角色屬性")]
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

    }

    // Update is called once per frame
    void Update()
    {

        //傷害先進行計算
        //damage = CharacterState.Player_ATK;
        ATK = CharacterState.Player_ATK;
        CRI = CharacterState.Player_CRI;
        CSD = CharacterState.Player_CSD;
        //CRI_Damage = Mathf.Round(damage * (CSD/100)) ;
    
    }

    //顯示傷害數字與判定是否爆擊
    public void showFloatDamage(float rate)
    {
        FloatDamagePool.instance.GetFormPool();

        if(rate < (CRI/100) )
        {        
            //isCrit = true;
        }
        else
        {
            //isCrit = false;
        }
    }



    //供參考
    /*private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            rate = Random.value;
            targetpos = other.gameObject.transform.GetChild(0).gameObject;

            FloatDamagePool.instance.GetFormPool();

            if(rate < (CRI/100) )
            {        
                isCrit = true;
            }
            else
            {
                isCrit = false;
            }
        }
    }*/
}

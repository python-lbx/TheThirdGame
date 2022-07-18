using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("機率")]
    public int CRI;
    public int CSD;
    public bool isCrit;
    public float rate;
    [Header("面板傷害")]
    public int damage;
    public float CRI_Damage;
    [Header("UI插件")]
    public Text CR_Text;
    public Text CDR_Text;
    [Header("目標物件")]
    public GameObject targetpos;
    public Player_Attributes CharacterState;
    // Start is called before the first frame update
    void Start()
    {
        CRI_Damage = damage *  (CSD/100);
    }

    // Update is called once per frame
    void Update()
    {
        CRI = CharacterState.Player_CRI;
        damage = CharacterState.Player_ATK;
        CRI_Damage = damage *  (CSD/100);
    }


    //有bug
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            rate = Random.value;
            targetpos = other.gameObject.transform.GetChild(0).gameObject;

            FloatDamagePool.instance.GetFormPool();

            if(rate < (CRI/100) )
            {
                CRI_Damage = damage *  (CSD/100);
                isCrit = true;
            }
            else
            {
                isCrit = false;
            }
        }
    }
}

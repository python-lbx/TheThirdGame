using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CDT : MonoBehaviour
{
    [Header("機率")]
    public int CR;
    public int CDR;
    public bool isCrit;
    public float rate;
    [Header("面板傷害")]
    public int damage;
    public float C_Damage;
    [Header("UI插件")]
    public Text CR_Text;
    public Text CDR_Text;
    [Header("目標物件")]
    public GameObject targetpos;
    // Start is called before the first frame update
    void Start()
    {
        C_Damage = Mathf.Round( damage * ( 1 + (CDR/100) ) )   ; // (1+ 5/100) = 1.05 暴擊傷害為105%
        rate = Random.value;
        if(rate < CR)
        {
            isCrit = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CR_Text.text = CR.ToString();
        CDR_Text.text = (100+CDR).ToString(); //100 + 5 = 105 面板顯示暴擊傷害為105%
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            //print(other.gameObject.transform.GetChild(0).transform.position); //目標位置

            targetpos = other.gameObject.transform.GetChild(0).gameObject;

            FloatDamagePool.instance.GetFormPool();

            //print(Random.value);
            if(Random.value < (CR/100)) // 5/100 = 0.05 暴擊率為5%  //暴擊
            {
                C_Damage = Mathf.Round( damage * ( 1 + (CDR/100) ) )   ; // (1+ 5/100) = 1.05 暴擊傷害為105%
                isCrit = true;
            }
            else
            {
                isCrit = false;
            }
        }    
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    Rigidbody2D rb;
    public int speed;
    [Header("角色屬性")]
    public PlayerController playercontroller;
    public float ATK;
    public float CRI;
    public float CSD;
    public bool isCrit;
    //public float rate;
    [Header("手里劍面板傷害")]
    public float Nor_damage; //基礎傷害
    public float CRI_Damage; //爆擊傷害
    public float damagerate; //傷害比例

    [Header("傷害浮動")]
    public GameObject floatdamagetext;

    private void Awake() 
    {
        //很重要
        playercontroller = FindObjectOfType<PlayerController>();        
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.right * speed;

    }

    // Update is called once per frame
    void Update()
    {
        //讀取角色屬性
        ATK = playercontroller.ATK;
        CRI = playercontroller.CRI;
        CSD = playercontroller.CSD;
        //進行計算
        Nor_damage = Mathf.Round(ATK * damagerate); //攻擊力X傷害比例
        CRI_Damage = Mathf.Round(ATK * damagerate * (CSD/100) ); //攻擊力X傷害比例X爆擊傷害
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            floatdamagetext = FloatDamagePool.instance.GetFormPool();

            if(floatdamagetext != null)
            {
                //var floatdamage = GetComponent<FloatDamageText>().floatdamage;

                float rate = Random.value;
                print(rate);

                floatdamagetext.transform.position = other.gameObject.transform.GetChild(0).transform.position;
                
                //floatdamagetext.GetComponent<FloatDamageText>().floatdamage.text = damagerate.ToString();

                if(rate < (CRI/100))
                {
                    floatdamagetext.GetComponent<FloatDamageText>().floatdamage.color = Color.red;
                    floatdamagetext.GetComponent<FloatDamageText>().floatdamage.fontSize = 30;
                    floatdamagetext.GetComponent<FloatDamageText>().floatdamage.text = CRI_Damage.ToString();
                }
                else
                {
                    floatdamagetext.GetComponent<FloatDamageText>().floatdamage.color = new Color(1,0.510174811f,0.00471699238f,255);
                    floatdamagetext.GetComponent<FloatDamageText>().floatdamage.fontSize = 20;
                    floatdamagetext.GetComponent<FloatDamageText>().floatdamage.text = Nor_damage.ToString();
                }
            }

    





            
            //floatdamage.currentdamage = playercontroller.damage * damagerate;

            //playercontroller.targetpos = other.gameObject.transform.GetChild(0).gameObject;

            //playercontroller.showFloatDamage(rate);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    Rigidbody2D rb;
    [Header("角色屬性")]
    public PlayerController playercontroller;
    public float ATK;
    public float CRI;
    public float CSD;
    public bool isCrit;

    [Header("雷切面板傷害")]
    public float Nor_damage; //基礎傷害
    public float CRI_Damage; //爆擊傷害
    public float damagerate; //傷害比例    

    [Header("傷害浮動")]
    public GameObject floatdamagetext;    

    public Collider2D L_Attack_Box;

    // Start is called before the first frame update
    private void Awake() 
    {
        playercontroller = FindObjectOfType<PlayerController>();       

        //很重要 
        ATK = playercontroller.ATK;
        CRI = playercontroller.CRI;
        CSD = playercontroller.CSD;
        //進行計算
        Nor_damage = Mathf.Round(ATK * damagerate); //攻擊力X傷害比例
        CRI_Damage = Mathf.Round(ATK * damagerate * (CSD/100) ); //攻擊力X傷害比例X爆擊傷害

    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right;
        Destroy(this.gameObject,1f);
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
                float rate = Random.value; //隨機機率
                print(rate);

                floatdamagetext.transform.position = other.gameObject.transform.Find("FloatDamagePoint").transform.position;
                
                if(rate < (CRI/100))
                {
                    other.gameObject.GetComponent<EnemyController>().GetDamage(CRI_Damage);
                    floatdamagetext.GetComponent<FloatDamageText>().floatdamage.color = Color.red;
                    floatdamagetext.GetComponent<FloatDamageText>().floatdamage.fontSize = 30;
                    floatdamagetext.GetComponent<FloatDamageText>().floatdamage.text = CRI_Damage.ToString();
                }
                else
                {
                    other.gameObject.GetComponent<EnemyController>().GetDamage(Nor_damage);
                    floatdamagetext.GetComponent<FloatDamageText>().floatdamage.color = new Color(1,0.510174811f,0.00471699238f,255);
                    floatdamagetext.GetComponent<FloatDamageText>().floatdamage.fontSize = 20;
                    floatdamagetext.GetComponent<FloatDamageText>().floatdamage.text = Nor_damage.ToString();
                }
            }
        }
    }



    public void SetActiveFalse()
    {
        this.gameObject.SetActive(false);
    }

    void ZboxActive()
    {
        L_Attack_Box.GetComponent<BoxCollider2D>().enabled = true;
    }

    void ZboxUnActive()
    {
        L_Attack_Box.GetComponent<BoxCollider2D>().enabled = false;
    }
    
}

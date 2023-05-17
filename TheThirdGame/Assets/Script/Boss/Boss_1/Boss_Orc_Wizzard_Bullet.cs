using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Orc_Wizzard_Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    public EnemyController enemycontroller;

    [Header("攻擊目標")]
    public GameObject Target;
    Vector2 Direction;
    Vector2 targetpos;
    public float focustime;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Target = GameObject.Find("Player"); 

        Invoke("ReadyToChase",0.25f); //定位
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetpos = Target.transform.position;

        Direction = targetpos - (Vector2)transform.position;
        
        if(focustime > 0)
        {
            focustime -= Time.deltaTime;

            transform.right = Direction;
        
        }
        else
        {
            rb.velocity = transform.right * speed;
        }   
    }

    void ReadyToChase()
    {
        rb.velocity = new Vector2(0,0);

    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInChildren<PlayerController>().GetDamage(enemycontroller.ATK); //對玩家造成傷害
            var floatdamage = FloatDamagePool.instance.GetFormPool(); //生成傷害浮動點數
            floatdamage.transform.position = other.gameObject.transform.Find("FloatDamagePoint").transform.position; //傷害浮動點數位置
            floatdamage.GetComponent<FloatDamageText>().floatdamage.color = new Color(1,0.510174811f,0.00471699238f,255); //設定顏色
            floatdamage.GetComponent<FloatDamageText>().floatdamage.fontSize = 20;
            floatdamage.GetComponent<FloatDamageText>().floatdamage.text = enemycontroller.ATK.ToString(); //傷害浮動點數輸出數字
        }
    }

}

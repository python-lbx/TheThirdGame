using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy_Ball : MonoBehaviour
{
    public GameObject boss;
    public GameObject FloatDamagePoint;
    public float speed;
    public Vector3 startPos;
    public float damage;
    public int HP;

    private void Start() 
    {
        startPos = transform.localPosition;
        this.gameObject.SetActive(false);
    }
    
    private void Update() 
    {
        transform.position = Vector2.MoveTowards(transform.position,boss.transform.position,speed * Time.deltaTime);

        if(HP <= 0)
        {
            ResPos();
        }
    }

    public void ResPos()
    {
        this.gameObject.SetActive(false);
        HP = 3;
        transform.localPosition = startPos;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {   
        //要改
        if(other.gameObject.name == "CrushWave")
        {
            print("Boss");
            other.GetComponentInParent<Boss_Level_3>().ballamount ++;
            ResPos();
        }

        if(other.gameObject.name == "Z-AttackBox")
        {
            HP--;

            Cut_Pool.instance.GetFormPool(this.gameObject.transform);

            var floatdamagetext = FloatDamagePool.instance.GetFormPool();

            floatdamagetext.transform.position = FloatDamagePoint.transform.position;
            floatdamagetext.GetComponent<FloatDamageText>().floatdamage.color = Color.red;
            floatdamagetext.GetComponent<FloatDamageText>().floatdamage.fontSize = 30;
            floatdamagetext.GetComponent<FloatDamageText>().floatdamage.text = "1".ToString();
        }

        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInChildren<PlayerController>().GetDamage(damage);
            var floatdamage = FloatDamagePool.instance.GetFormPool(); //生成傷害浮動點數
            floatdamage.transform.position = other.gameObject.transform.Find("FloatDamagePoint").transform.position; //傷害浮動點數位置
            floatdamage.GetComponent<FloatDamageText>().floatdamage.color = new Color(1,0.510174811f,0.00471699238f,255); //設定顏色
            floatdamage.GetComponent<FloatDamageText>().floatdamage.fontSize = 20;
            floatdamage.GetComponent<FloatDamageText>().floatdamage.text = damage.ToString(); //傷害浮動點數輸出數字
        }
    }
}

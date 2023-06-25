using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Bullet_Prefab : MonoBehaviour
{
    public float activeTime;
    public float activeStart;

    public EnemyController enemycontroller;
    // Start is called before the first frame update

    private void OnEnable() 
    {
        activeStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= activeStart + activeTime) //生成時間過後消失
        {
            Orc_Bullet_Pool.instance.ReturnPool(this.gameObject);
        }    
        else if(enemycontroller.currenthealth <= 0)
        {
            Orc_Bullet_Pool.instance.ReturnPool(this.gameObject);
        }
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

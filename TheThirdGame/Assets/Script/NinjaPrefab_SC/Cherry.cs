using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{   
    public float activeTime;
    public float activeStart;
    public float heal;

    private void OnEnable() 
    {
        activeStart = Time.time;
    }
    private void Update() 
    {
        if(Time.time >= activeStart + activeTime) //生成時間過後消失
        {
            Cherry_Pool.instance.ReturnPool(this.gameObject); //改名
        }  
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            AVmanager.instance.Play("Recover");
            other.gameObject.GetComponentInChildren<PlayerController>().GetHeal(heal);
            var floatdamage = FloatDamagePool.instance.GetFormPool(); //生成治療浮動點數
            floatdamage.transform.position = other.gameObject.transform.Find("FloatDamagePoint").transform.position; //傷害浮動點數位置
            floatdamage.GetComponent<FloatDamageText>().floatdamage.color = Color.green; //設定顏色
            floatdamage.GetComponent<FloatDamageText>().floatdamage.fontSize = 20;
            floatdamage.GetComponent<FloatDamageText>().floatdamage.text = heal.ToString(); //治療浮動點數輸出數字 

            //生成特效
            Heal_Cross_Pool.instance.GetFormPool(other.gameObject.transform,other.gameObject);
            
            //回收
            Cherry_Pool.instance.ReturnPool(this.gameObject);
        }
    }
}

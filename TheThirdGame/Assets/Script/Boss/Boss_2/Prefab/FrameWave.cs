using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameWave : MonoBehaviour
{
    public float damage;
    public float activeTime;
    public float activeStart;
    private void OnEnable()
    {
        activeStart = Time.time;
    }

    // Start is called before the first frame update
    void Update()
    {
        if(Time.time >= activeStart + activeTime) //生成時間過後消失
        {
            MoveWave_Pool.instance.ReturnPool(this.gameObject);
        }    
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
        other.gameObject.GetComponentInChildren<PlayerController>().GetDamage(damage); //對玩家造成傷害
        var floatdamage = FloatDamagePool.instance.GetFormPool(); //生成傷害浮動點數
        floatdamage.transform.position = other.gameObject.transform.Find("FloatDamagePoint").transform.position; //傷害浮動點數位置
        floatdamage.GetComponent<FloatDamageText>().floatdamage.color = new Color(1,0.510174811f,0.00471699238f,255); //設定顏色
        floatdamage.GetComponent<FloatDamageText>().floatdamage.fontSize = 20;
        floatdamage.GetComponent<FloatDamageText>().floatdamage.text = damage.ToString(); //傷害浮動點數輸出數字
        }
    }
}

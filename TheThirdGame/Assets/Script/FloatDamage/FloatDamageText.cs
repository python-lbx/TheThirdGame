using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatDamageText : MonoBehaviour
{    
    Rigidbody2D rb;
    
    public int speed; //字體上升速度

    public float activeTime;
    public float activeStart;

    public PlayerController CRI_target;
    public Text floatdamage;
    public Color color;


    private void OnDisable() 
    {        
        rb = GetComponent<Rigidbody2D>();
        CRI_target = FindObjectOfType<PlayerController>();
    }
    
    private void OnEnable() 
    {   
        if(CRI_target != null)
        {
            if(CRI_target.isCrit) //暴擊 紅字 變大
            {
                floatdamage.color = Color.red;
                floatdamage.fontSize = 30;
                floatdamage.text = CRI_target.CRI_Damage.ToString();
            }
            else //無暴擊 橙字 正常
            {   
                floatdamage.color = new Color(1,0.510174811f,0.00471699238f,255);
                floatdamage.fontSize = 20;
                floatdamage.text = CRI_target.damage.ToString();
            }

        }


        activeStart = Time.time; //生成時間
        if(CRI_target != null)
        {
        transform.position = CRI_target.targetpos.transform.position; //生成位置
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0,speed); //向上

        if(Time.time >= activeStart + activeTime) //生成時間過後消失
        {
            FloatDamagePool.instance.ReturnPool(this.gameObject);
        }
    }
    
}

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

    //public PlayerController playercontroller;
    //public FloatDamagePool floatdamagepool;
    public Text floatdamage;
    public Color color;
    public Camera maincamera;

    private void OnDisable() 
    {        
        rb = GetComponent<Rigidbody2D>();
        maincamera = Camera.main;
        transform.GetChild(0).GetComponent<Canvas>().worldCamera = maincamera;
        //playercontroller = FindObjectOfType<PlayerController>();
        //floatdamagepool = FindObjectOfType<FloatDamagePool>();
    }
    
    private void OnEnable() 
    {   
        activeStart = Time.time; //生成時間

    #region  廢案
        /*if(playercontroller != null)
        {
            if(playercontroller.isCrit) //暴擊 紅字 變大
            {
                floatdamage.color = Color.red;
                floatdamage.fontSize = 30;
                floatdamage.text = playercontroller.CRI_Damage.ToString();
            }
            else //無暴擊 橙字 正常
            {   
                floatdamage.text = floatdamagepool.damageRecord.ToString();
                floatdamage.color = new Color(1,0.510174811f,0.00471699238f,255);
                floatdamage.fontSize = 20;
            }

        }*/


        
        /*if(playercontroller != null)
        {
        transform.position = playercontroller.targetpos.transform.position; //生成位置
        }*/
        #endregion
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

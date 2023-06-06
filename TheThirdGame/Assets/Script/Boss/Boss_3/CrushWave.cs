using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushWave : MonoBehaviour
{
    Animator anim;
    SpriteRenderer SR;
    public GameObject Boss3;
    public Color r0;
    public Color r1;
    public Color r2;
    public Color r3;
    public Color r4;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(Boss3.GetComponent<Boss_Level_3>().ballamount)
        {
            case 0:
            SR.color = r0;
            break;

            case 1:
            SR.color = r1;
            break;

            case 2:
            SR.color = r2;
            break;

            case 3:
            SR.color = r3;
            break;

            case 4:
            SR.color = r4;
            break;
        }

        if(Boss3.GetComponent<Boss_Level_3>().ballamount == 4)
        {
            anim.SetBool("Start",true);
        }
    }

    public void Reset() 
    {   
        anim.SetBool("Start",false);
        Boss3.GetComponent<Boss_Level_3>().ballamount = 0;
        this.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "NPC")
        {
            other.GetComponent<NPC>().HP--;
        }


        if(Boss3.GetComponent<Boss_Level_3>().ballamount == 4)
        {
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
}
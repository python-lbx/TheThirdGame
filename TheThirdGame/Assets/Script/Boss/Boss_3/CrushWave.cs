using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushWave : MonoBehaviour
{
    Animator anim;
    SpriteRenderer SR;
    public Boss_Level_3 Boss3;
    public Color r0;
    public Color r1;
    public Color r2;
    public Color r3;
    public Color r4;
    public float damage;

    // Start is called before the first frame update

    void OnEnable() 
    {
        Boss3.ballamount = 0; //吃球數歸0
        transform.localScale = new Vector3(0.6f,0.6f,1); //尺寸重置 避免二次傷害
    }

    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        switch(Boss3.ballamount)
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

        // if(Boss3.ballamount == 4)
        // {
        //     anim.SetBool("Start",true);
        // }
    }

    public void Reset() 
    {   
        anim.SetBool("Start",false);

        //歸0
        Boss3.ballamount = 0;

        //轉階段
        Boss3.PhaseTime = 3.5f;
        Boss3.SkillPhase ++;
        Boss3.current_Statue = Boss_Level_3.Statue.Idle;

        this.gameObject.SetActive(false);
    }

    public void voice()
    {
        AVmanager.instance.Play("Wizard_FireSpell_5");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "NPC")
        {
            other.GetComponent<NPC>().GetDamage(1);
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

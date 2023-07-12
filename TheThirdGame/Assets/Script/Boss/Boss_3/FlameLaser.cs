using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameLaser : MonoBehaviour
{
    Animator anim;
    public Animator b_anim;
    Collider2D coll;
    public Boss_Level_3 Boss;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        Boss = GetComponentInParent<Boss_Level_3>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "shield")
        {
            other.gameObject.SetActive(false);
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

    private void Reset() 
    {
        coll.enabled = false;
        Boss.focustime = Boss.RechargeTime;
        Boss.shoottime++;
        anim.SetBool("Start",false);

        if(Boss.shoottime == 4)
        {
            this.gameObject.SetActive(false);
            this.gameObject.transform.localScale = new Vector3(1,1,1);
        }
    }

    void activecoll()
    {
        coll.enabled = true;
        b_anim.SetTrigger("Attack");
    }

    void Attack_anim_Trigger()
    {
        AVmanager.instance.Play("Wizard_FireSpell_3");
    }
}

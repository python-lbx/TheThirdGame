using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Wizzard_State : MonoBehaviour
{
    [Header("當前階段")]
    public Statue current_Statue;
    public float fade = 0f;
    public enum Statue{Ready,Fight,Dead}
    [Header("角色腳本")]
    public Boss_Orc_Wizzard boss_Orc_Wizzard;
    public EnemyController enemyController;

    [Header("角色渲染")]
    public Material material;
    public GameObject portal;
    Rigidbody2D rb;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        enemyController = GetComponent<EnemyController>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boss_Orc_Wizzard.enabled = false;
        
        current_Statue = Statue.Ready;
    }

    // Update is called once per frame
    void Update()
    {
        material.SetFloat("_Fade",fade);//控制數值

        switch (current_Statue)
        {
            case Statue.Ready:
            gameObject.layer = LayerMask.NameToLayer("Invincible");

            fade += Time.deltaTime * 0.5f;

            if(fade >= 1f)
            {
                fade = 1;
                current_Statue = Statue.Fight;
            }

            break;

            case Statue.Fight:
            gameObject.layer = LayerMask.NameToLayer("Enemy");
            boss_Orc_Wizzard.enabled = true;

            if(enemyController.currenthealth <= 0)
            {                
                current_Statue = Statue.Dead;
            }


            break;

            case Statue.Dead:
            gameObject.layer = LayerMask.NameToLayer("Invincible");

            boss_Orc_Wizzard.current_Statue = Boss_Orc_Wizzard.Statue.Idle;
            boss_Orc_Wizzard.PhaseTime = 10f;
            boss_Orc_Wizzard.speed = 0;
            rb.velocity = new Vector2(0,0);
            anim.SetBool("Run",false);

            boss_Orc_Wizzard.DialogTable.SetActive(true);
            boss_Orc_Wizzard.Dialog.text = "我在虛無等你...";


            fade -= Time.deltaTime * 0.5f;

            if(fade <= 0)
            {
                fade = 0;
                boss_Orc_Wizzard.DialogTable.SetActive(false);
                boss_Orc_Wizzard.Dialog.text = "";
                boss_Orc_Wizzard.enabled = false;
                portal.SetActive(true);
                Destroy(this.gameObject);
            }

            break;
        }
    }
}

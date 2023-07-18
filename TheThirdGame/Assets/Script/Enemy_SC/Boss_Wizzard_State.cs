using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Wizzard_State : MonoBehaviour
{
    [Header("當前階段")]
    public Statue current_Statue;
    public float fade = 0f;
    public enum Statue{Ready,Fight,Dead}
    [Header("角色腳本")]
    //public Boss_Orc_Wizzard boss_Orc_Wizzard; 一開始的寫法
    public boss Boss; //新的
    public enum boss {One,Two,Three}; //新的
    public Boss_Orc_Wizzard boss_I; //新的
    public Boss_Level_2 boss_II; //新的
    public Boss_Level_3 boss_III; //新的
    public EnemyController enemyController;

    [Header("角色渲染")]
    public Material material;
    public GameObject portal;
    public GameObject chest;
    //Rigidbody2D rb;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        enemyController = GetComponent<EnemyController>();
        //rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();


        //boss_Orc_Wizzard.enabled = false; 一開始的寫法

        //新的
        switch(Boss)
        {
            case boss.One:
            boss_I.enabled = false;
            break;

            case boss.Two:
            boss_II.enabled = false;
            break;

            case boss.Three:
            boss_III.enabled = false;
            break;
        }
        
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

            //boss_Orc_Wizzard.enabled = true; 一開始的寫法

            //新的
            switch(Boss)
            {
                case boss.One:
                boss_I.enabled = true;
                break;

                case boss.Two:
                boss_II.enabled = true;
                break;

                case boss.Three:
                boss_III.enabled = true;
                break;
            }

            if(enemyController.currenthealth <= 0)
            {                
                current_Statue = Statue.Dead;
            }

            break;




            case Statue.Dead:
            //無法指定
            gameObject.layer = LayerMask.NameToLayer("Invincible");

            //停止動作 一開始的寫法
            // boss_Orc_Wizzard.current_Statue = Boss_Orc_Wizzard.Statue.Idle;
            // boss_Orc_Wizzard.PhaseTime = 10f;
            // boss_Orc_Wizzard.speed = 0;

            //王狀態為死亡
            switch(Boss)
            {
                case boss.One:
                boss_I.current_Statue = Boss_Orc_Wizzard.Statue.GameOver;
                break;

                case boss.Two:
                boss_II.current_Statue = Boss_Level_2.Statue.GameOver;
                break;

                case boss.Three:
                boss_III.current_Statue = Boss_Level_3.Statue.GameOver;
                break;
            }

            //rb.velocity = new Vector2(0,0);

            //對話內容 一開始的寫法
            // boss_Orc_Wizzard.DialogTable.SetActive(true);
            // boss_Orc_Wizzard.Dialog.text = "我在虛無等你...";

             //王對死亡對白
            switch(Boss)
            {
                case boss.One:
                boss_I.DialogTable.SetActive(true);
                boss_I.Dialog.text = "waiting for you in nothingness...";
                break;

                case boss.Two:
                boss_II.DialogTable.SetActive(true);
                boss_II.Dialog.text = "Nothingness is summoning you...";
                break;

                case boss.Three:
                boss_III.DialogTable.SetActive(true);
                boss_III.Dialog.text = "The void...betrayed...me";                
                break;
            }

            //漸變
            //漸變結束後關閉對話框
            fade -= Time.deltaTime * 0.5f;

            if(fade <= 0)
            {
                fade = 0;

                //對話內容 一開始的寫法
                // boss_Orc_Wizzard.DialogTable.SetActive(false);
                // boss_Orc_Wizzard.Dialog.text = "";

                //新的
                switch(Boss)
                {
                    case boss.One:
                    boss_I.DialogTable.SetActive(false);
                    boss_I.Dialog.text = "";
                    break;

                    case boss.Two:
                    boss_II.DialogTable.SetActive(false);
                    boss_II.Dialog.text = "";
                    break;

                    case boss.Three:
                    boss_III.DialogTable.SetActive(false);
                    boss_III.Dialog.text = "";
                    break;
                }

                //腳本停止 一開始的寫法
                //boss_Orc_Wizzard.enabled = false;

                //新的
                switch(Boss)
                {
                    case boss.One:
                    boss_I.enabled = false;
                    break;

                    case boss.Two:
                    boss_II.enabled = false;
                    break;

                    case boss.Three:
                    boss_III.enabled = false;
                    break;
                }


                //寶箱內容
                chest.SetActive(true);
                chest.GetComponent<Chest>().DropTime = 10;

                //傳送門
                portal.SetActive(true);

                //對象不顯示
                this.gameObject.SetActive(false);
            }

            break;
        }
    }
}

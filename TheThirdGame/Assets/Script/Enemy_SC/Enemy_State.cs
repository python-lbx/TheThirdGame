using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_State : MonoBehaviour
{
    [Header("當前階段")]
    public Statue current_Statue;
    public float fade = 0f;
    public enum Statue{Ready,Fight,Dead,Idle}
    public string LayerName;
    [Header("角色腳本")]
    //public ScriptStatue whichCharacter;
    //public enum ScriptStatue{Blade,Boo,Wizzard,Fly}
    // public Orc_Blade orc_Blade;
    // public Orc_Boo orc_Boo;
    // public Orc_Wizzard orc_Wizzard;
    // public Orc_Fly orc_Fly;
    public EnemyController enemyController;
    [Header("角色渲染")]
    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        enemyController = GetComponent<EnemyController>();

        current_Statue = Statue.Ready;

        // switch (whichCharacter)
        // {
        //     case ScriptStatue.Blade:
        //     orc_Blade = GetComponent<Orc_Blade>();
        //     orc_Blade.enabled = false;
        //     break;
        //     case ScriptStatue.Boo:
        //     orc_Boo = GetComponent<Orc_Boo>();
        //     orc_Boo.enabled = false;
        //     break;
        //     case ScriptStatue.Wizzard:
        //     orc_Wizzard = GetComponent<Orc_Wizzard>();
        //     orc_Wizzard.enabled = false;
        //     break;
        //     case ScriptStatue.Fly:
        //     orc_Fly = GetComponent<Orc_Fly>();
        //     orc_Fly.enabled = false;
        //     break;
        // }
    }

    // Update is called once per frame
    void Update()
    {
        material.SetFloat("_Fade",fade);//控制數值

        switch (current_Statue)
        {
            case Statue.Ready:
            gameObject.layer = LayerMask.NameToLayer("Invincible");

            fade += Time.deltaTime;

            if(fade >= 1f)
            {
                fade = 1;
                current_Statue = Statue.Fight;
            }
            
            break;

            case Statue.Fight:
            //gameObject.layer = LayerMask.NameToLayer("Enemy");

            // switch (whichCharacter)
            // {
            //     case ScriptStatue.Blade:
            //     orc_Blade = GetComponent<Orc_Blade>();
            //     orc_Blade.enabled = true;
            //     break;
            //     case ScriptStatue.Boo:
            //     orc_Boo = GetComponent<Orc_Boo>();
            //     orc_Boo.enabled = true;
            //     break;
            //     case ScriptStatue.Wizzard:
            //     orc_Wizzard = GetComponent<Orc_Wizzard>();
            //     orc_Wizzard.enabled = true;
            //     break;
            //     case ScriptStatue.Fly:
            //     orc_Fly = GetComponent<Orc_Fly>();
            //     orc_Fly.enabled = true;
            //     break;
            // }

            // if(enemyController.currenthealth <= 0)
            // {
            //     current_Statue = Statue.Dead;
            // }

            break;

            case Statue.Dead:

            gameObject.layer = LayerMask.NameToLayer("Invincible");

            // switch (whichCharacter)
            // {
            //     case ScriptStatue.Blade:
            //     orc_Blade = GetComponent<Orc_Blade>();
            //     orc_Blade.enabled = false;
            //     break;
            //     case ScriptStatue.Boo:
            //     orc_Boo = GetComponent<Orc_Boo>();
            //     orc_Boo.enabled = false;
            //     break;
            //     case ScriptStatue.Wizzard:
            //     orc_Wizzard = GetComponent<Orc_Wizzard>();
            //     orc_Wizzard.enabled = false;
            //     break;
            //     case ScriptStatue.Fly:
            //     orc_Fly = GetComponent<Orc_Fly>();
            //     orc_Fly.enabled = false;
            //     break;
            // }

            fade -= Time.deltaTime;

            if(fade <= 0)
            {
                fade = 0;
                Destroy(this.gameObject);
            }

            break;

            case Statue.Idle:
            gameObject.layer = LayerMask.NameToLayer(LayerName);
            
            if(enemyController.currenthealth <= 0)
            {
                current_Statue = Statue.Dead;
            }

            break;
        }
    }
}

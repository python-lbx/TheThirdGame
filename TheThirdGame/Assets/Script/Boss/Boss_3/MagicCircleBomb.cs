using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircleBomb : MonoBehaviour
{
    public bool Player;
    public float damage;
    public GameObject _NPC;
    public GameObject player;
    public GameObject Circle;

    // Start is called before the first frame update

    void Start()
    {
        _NPC = gameObject.transform.GetComponentInParent<NPC>().gameObject;
        var main = GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleSystemStopped()
    {
        Debug.Log("System has stopped!");

        if(Player)
        {
            player.GetComponentInChildren<PlayerController>().GetDamage(damage);
            var floatdamage = FloatDamagePool.instance.GetFormPool(); //生成傷害浮動點數
            floatdamage.transform.position = player.transform.Find("FloatDamagePoint").transform.position; //傷害浮動點數位置
            floatdamage.GetComponent<FloatDamageText>().floatdamage.color = new Color(1,0.510174811f,0.00471699238f,255); //設定顏色
            floatdamage.GetComponent<FloatDamageText>().floatdamage.fontSize = 20;
            floatdamage.GetComponent<FloatDamageText>().floatdamage.text = damage.ToString(); //傷害浮動點數輸出數字
        }
        else
        {
            _NPC.GetComponent<NPC>().HP --;
        }

        Circle.SetActive(false);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Player = true;
            player = other.gameObject;
        }
    }
    

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Player = false;
            player = null;
        }
    }
}

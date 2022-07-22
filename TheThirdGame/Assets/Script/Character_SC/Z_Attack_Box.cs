using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Z_Attack_Box : MonoBehaviour
{
    public float damagerate;
    public PlayerController playcontroller;
    public FloatDamagePool floatdamagepool;
    // Start is called before the first frame update
    private void Awake() 
    {
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Enemy"))
        {   

            floatdamagepool.damageRecord = playcontroller.damage * damagerate;

            //普攻的爆擊機率
            float rate = Random.value;

            //打到的目標轉入
            playcontroller.targetpos = other.gameObject.transform.GetChild(0).gameObject;
            
            //計算是否爆擊
            playcontroller.showFloatDamage(rate);
        }
    }
}

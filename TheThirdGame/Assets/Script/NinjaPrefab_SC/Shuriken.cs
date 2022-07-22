using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    Rigidbody2D rb;
    public int speed;
    public float damagerate;
    public PlayerController playercontroller;
    public FloatDamageText floatdamage;
    // Start is called before the first frame update
    private void Awake() 
    {
        playercontroller = FindObjectOfType<PlayerController>();
        floatdamage = FindObjectOfType<FloatDamageText>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        rb.velocity = transform.right * speed;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            float rate = Random.value;
            
            //floatdamage.currentdamage = playercontroller.damage * damagerate;

            playercontroller.targetpos = other.gameObject.transform.GetChild(0).gameObject;

            playercontroller.showFloatDamage(rate);
        }
    }
}

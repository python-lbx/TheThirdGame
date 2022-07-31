using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Orc_Wizzard_Bullet : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("攻擊目標")]
    public GameObject Target;
    Vector2 Direction;
    Vector2 targetpos;
    public float focustime;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Target = GameObject.Find("Player"); 

        Invoke("ReadyToChase",0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetpos = Target.transform.position;

        Direction = targetpos - (Vector2)transform.position;
        
        if(focustime > 0)
        {
            focustime -= Time.deltaTime;

            transform.right = Direction;
        
        }
        else
        {
            rb.velocity = transform.right * speed;
        }   
    }

    void ReadyToChase()
    {
        rb.velocity = new Vector2(0,0);

    }
}

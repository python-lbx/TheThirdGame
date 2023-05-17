using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Fire : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public GameObject FireBall;
    public float shootTime;
    public float shootcd;
    public float teleportcd;
    public float teleporttime;
    public int num;
    public int lastnum;
    public Transform[] Teleport;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(teleporttime > 0)
        {
            teleporttime -= Time.deltaTime;
        }
        else if(teleporttime <= 0)
        {
            num = Random.Range(0,3);
            lastnum = num;
            if(lastnum == num)
            {
                num = Random.Range(0,3);
            }
            else
            {
                teleporttime = teleportcd;
                transform.position = Teleport[num].position;
            }
        }
    }

    public void FireBallShoot()
    {
        rb.velocity = new Vector2(speed,0f);
        if(teleporttime > 0)
        {
            shootTime -= Time.deltaTime;
        }
        else if(shootTime <= 0)
        {
            shootTime = shootcd;
            Instantiate(FireBall,transform.position,Quaternion.identity);
        }
    }
}

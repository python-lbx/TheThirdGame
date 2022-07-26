using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Boo : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    [Header("狀態數值")]
    //public float speed;
    public bool faceright;

    [Header("攻擊目標")]
    public GameObject Target;
    public GameObject boomerang;
    public GameObject shootPoint;
    Vector2 Direction;
    Vector2 targetpos;
    public float focustime;
    
    [Header("階段")]
    public Statue statue;
    public enum Statue{Focus,Shoot};
    //public float PhaseTime;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Target = GameObject.FindGameObjectWithTag("Player");
        Direction = Target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (statue)
        {
            case Statue.Focus:
            targetpos = Target.transform.position;

            Direction = targetpos - (Vector2)shootPoint.transform.position;
            

            if(focustime > 0)
            {
                focustime -= Time.deltaTime;
                
                shootPoint.transform.right = Direction;

                if(Target.transform.position.x < transform.position.x && faceright)
                {
                    faceright = false;
                    transform.Rotate(0,180,0);
                    //print("on your left");
                }
                else if(Target.transform.position.x > transform.position.x && !faceright)
                {
                    faceright = true;
                    transform.Rotate(0,180,0);
                    //print("on your right");
                }  

            }
            else
            {
                statue = Statue.Shoot;
            }

            break;

            case Statue.Shoot:
            anim.SetTrigger("Attack");
            focustime = 2f;
            statue = Statue.Focus;
            break;
        }
    }

    public void shootboo()
    {
        var boo = Instantiate(boomerang,shootPoint.transform.position,Quaternion.identity);
        boo.GetComponent<Rigidbody2D>().velocity = shootPoint.transform.right * boo.GetComponent<Boomerang>().speed;
    }
}

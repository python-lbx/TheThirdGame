using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Orc_Wizzard : MonoBehaviour
{
    Animator anim;
    public GameObject[] TransPoint;
    public int TransTime;
    public float time;

    public enum Statue{Idle,Patrol,Shoot,Trans,Wave}
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(time > 0 && TransTime < 5)
        {
            time -= Time.deltaTime;
        }
        else if(time <= 0 )
        {
            anim.SetTrigger("Attack");
            time = 1.5f;
            transform.position = TransPoint[TransTime].transform.position;
            TransTime++;

        }
    }
}

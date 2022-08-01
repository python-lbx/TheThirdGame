using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Orc_Wizzard : MonoBehaviour
{
    Animator anim;
    public GameObject[] TransPoint;
    public int TransTime;
    public float time;

    [Header("角色面向")]
    public bool faceright;
    [Header("移動速度")]
    public float speed;
    [Header("移動範圍")]
    public Transform leftPoint;
    public Transform rightPoint;




    [Header("當前階段")]
    public Statue current_Statue;
    public enum Statue{Idle,Patrol,Shoot,Trans,Wave}
    [Header("下個技能階段")]
    public int SkillPhase;
    public int SkillTime;
    public Statue Next_Skill_Statue;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (SkillPhase)
        {
            case 0:
            Next_Skill_Statue = Statue.Shoot;
            break;

            case 1:
            Next_Skill_Statue = Statue.Trans;
            break;

            case 2:
            Next_Skill_Statue = Statue.Wave;
            break;

            case 3:
            Next_Skill_Statue = Statue.Patrol;
            break;
        }



        switch (current_Statue)
        {
            case Statue.Idle:
            break;
            case Statue.Patrol:
            break;
            case Statue.Shoot: //3
            break;
            case Statue.Trans: //5
            break;
            case Statue.Wave: //4
            break;
        }

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

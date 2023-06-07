using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    SpriteRenderer SR;
    Animator anim;
    // public Color FullHP;
    // public Color HalfHP;
    // public Color EmptyHP;
    // public Color stuck;
    public GameObject shield;
    public bool bestuck;

    [Range(0, 2)]
    public int HP;
    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Stuck",shield.activeSelf);


        if(!shield.activeSelf)
        {
            anim.SetInteger("HP",HP);
        }
        
        // if(shield.activeSelf)
        // {
        //     SR.color = stuck;
        // }
        // else
        // {
        //     shield.SetActive(false);

        //     switch (HP)
        //     {
        //         case 2:
        //         SR.color = FullHP;
        //         break;

        //         case 1:
        //         SR.color = HalfHP;
        //         break;

        //         case 0:
        //         SR.color = EmptyHP;
        //         break;
        //     }
        // }


    }
}

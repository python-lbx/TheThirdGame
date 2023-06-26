using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_3_Inspector : MonoBehaviour
{
    public GameObject[] NPC = new GameObject[4];
    public bool[] died = new bool[4];
    public bool allalive;
    public GameObject Boss;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(NPC[0].GetComponent<NPC>().HP <= 0)
        {
            died[0] = true;
        }
        if(NPC[1].GetComponent<NPC>().HP <= 0)
        {
            died[1] = true;
        }        
        if(NPC[2].GetComponent<NPC>().HP <= 0)
        {
            died[2] = true;
        }        
        if(NPC[3].GetComponent<NPC>().HP <= 0)
        {
            died[3] = true;
        }

        for(int i = 0 ; i < died.Length ; i++)
        {
            if(!died[i])
            {
                allalive = true;
                break;
            }
            else
            {
                allalive = false;
            }
        }

        if(!allalive)
        {
            Boss.GetComponent<Boss_Level_3>().StopEveryThing();
            Boss.GetComponent<Boss_Level_3>().enabled = false;
            Boss.GetComponent<Boss_Wizzard_State>().enabled = false;
            Boss.layer = LayerMask.NameToLayer("Invincible");        
        }
    }


}

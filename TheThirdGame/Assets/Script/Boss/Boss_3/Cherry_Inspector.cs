using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry_Inspector : MonoBehaviour
{
    public GameObject[] NPC;
    public GameObject Boss;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach(var npc in NPC)
        {
            if(npc.GetComponent<NPC>().HP > 0)
            {
                break;
            }
            else
            {
                //print("GO");
                Boss.GetComponent<Boss_Level_3>().StopEveryThing();
                Boss.GetComponent<Boss_Level_3>().enabled = false;
                Boss.GetComponent<Boss_Wizzard_State>().enabled = false;
                Boss.layer = LayerMask.NameToLayer("Invincible");
            }
        }
    }
}

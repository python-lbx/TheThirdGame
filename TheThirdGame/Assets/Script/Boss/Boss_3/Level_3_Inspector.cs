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

        //遊戲結束
        if(!allalive) //居民全滅 
        {
            Boss.GetComponent<Boss_Level_3>().current_Statue = Boss_Level_3.Statue.GameOver;

            Boss.GetComponent<Boss_Level_3>().DialogTable.SetActive(true);
            Boss.GetComponent<Boss_Level_3>().Dialog.text = "你一個都救不了";
            
            Player.GetComponent<PlayerState>().current_Statue = PlayerState.Statue.Dead;

            Invoke("stopthisScript",2f);
        }
        else if(Player.GetComponent<PlayerState>().current_Statue == PlayerState.Statue.Dead) //玩家死亡 
        {
            Boss.GetComponent<Boss_Level_3>().current_Statue = Boss_Level_3.Statue.GameOver;

            Boss.GetComponent<Boss_Level_3>().DialogTable.SetActive(true);
            Boss.GetComponent<Boss_Level_3>().Dialog.text = "屈服於力量吧";

            Invoke("stopthisScript",2f);
        }

        
    }

    void stopthisScript()
    {
        Boss.GetComponent<Boss_Level_3>().enabled = false;
        Boss.GetComponent<Boss_Wizzard_State>().enabled = false;

        this.enabled = false;
    }


}

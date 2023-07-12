using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_2_Inspector : MonoBehaviour
{
    public GameObject Boss;
    public GameObject Player;

    // Update is called once per frame
    void Update()
    {
        if(Player.GetComponent<PlayerState>().current_Statue == PlayerState.Statue.Dead) //玩家死亡 
        {
            Boss.GetComponent<Boss_Level_2>().current_Statue = Boss_Level_2.Statue.GameOver;

            Boss.layer = LayerMask.NameToLayer("Invincible");   
            Boss.GetComponent<Boss_Level_2>().DialogTable.SetActive(true);
            Boss.GetComponent<Boss_Level_2>().Dialog.text = "Weak existence";

            Invoke("stopthisScript",2f);
        }    
    }

    void stopthisScript()
    {
        Boss.GetComponent<Boss_Level_2>().enabled = false;
        Boss.GetComponent<Boss_Wizzard_State>().enabled = false;

        this.enabled = false;
    }

}

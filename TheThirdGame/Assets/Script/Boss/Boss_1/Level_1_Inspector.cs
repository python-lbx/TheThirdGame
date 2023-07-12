using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_1_Inspector : MonoBehaviour
{
    public GameObject Boss;
    public GameObject Player;

    // Update is called once per frame
    void Update()
    {
        if(Player.GetComponent<PlayerState>().current_Statue == PlayerState.Statue.Dead) //玩家死亡 
        {
            Boss.GetComponent<Boss_Orc_Wizzard>().current_Statue = Boss_Orc_Wizzard.Statue.GameOver;

            Boss.layer = LayerMask.NameToLayer("Invincible");   
            Boss.GetComponent<Boss_Orc_Wizzard>().DialogTable.SetActive(true);
            Boss.GetComponent<Boss_Orc_Wizzard>().Dialog.text = "It's over";

            Invoke("stopthisScript",2f);
        }   
    }

    void stopthisScript()
    {
        Boss.GetComponent<Boss_Orc_Wizzard>().enabled = false;
        Boss.GetComponent<Boss_Wizzard_State>().enabled = false;

        this.enabled = false;
    }
}

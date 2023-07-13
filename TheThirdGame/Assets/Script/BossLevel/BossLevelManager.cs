using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevelManager : MonoBehaviour
{
    //Control Camera position and create Boss;
    //control portal active or not when boss was dead;
    // Start is called before the first frame update
    public BossResourceUI bossUI;
    public GameObject UI;
    public int level;
    public GameObject Boss;
    public GameObject portal;
    public bool firsttime = true;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {   
            AVmanager.instance.Stop("Level");

            if(firsttime)
            {
                AVmanager.instance.Play("BossStart");
                firsttime = false;
            }

            UI.SetActive(true);
            bossUI = UI.GetComponent<BossResourceUI>();
            switch(level)
            {
                case 1:
                bossUI.Boss = BossResourceUI.boss.One;
                AVmanager.instance.Play("Boss1");
                break;

                case 2:
                bossUI.Boss = BossResourceUI.boss.Two;
                AVmanager.instance.Play("Boss2");
                break;

                case 3:
                bossUI.Boss = BossResourceUI.boss.Three;
                AVmanager.instance.Play("Boss3");
                break;
            }

            FindObjectOfType<CameraController>().ChangeTarget(transform);
            
            if(Boss != null)
            {
                if(Boss.GetComponent<Boss_Wizzard_State>().current_Statue != Boss_Wizzard_State.Statue.Dead)
                {
                    portal.SetActive(false);
                    Boss.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            UI.SetActive(false);
            AVmanager.instance.Stop("Boss1");
            AVmanager.instance.Stop("Boss2");
            AVmanager.instance.Stop("Boss3");
            
            AVmanager.instance.Play("Level");
        }
    }
}

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

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {   
            UI.SetActive(true);
            bossUI = UI.GetComponent<BossResourceUI>();
            switch(level)
            {
                case 1:
                bossUI.Boss = BossResourceUI.boss.One;
                break;

                case 2:
                bossUI.Boss = BossResourceUI.boss.Two;
                break;

                case 3:
                bossUI.Boss = BossResourceUI.boss.Three;
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
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevelManager : MonoBehaviour
{
    //Control Camera position and create Boss;
    //control portal active or not when boss was dead;
    // Start is called before the first frame update
    public BossResourceUI bossUI;
    public int level;
    public GameObject Boss;
    public GameObject portal;
    void Start()
    {
        bossUI = FindObjectOfType<BossResourceUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
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
}

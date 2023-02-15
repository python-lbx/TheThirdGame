using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevelManager : MonoBehaviour
{
    //Control Camera position and create Boss;
    //control portal active or not when boss was dead;
    // Start is called before the first frame update
    public GameObject Boss;
    public GameObject portal;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
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

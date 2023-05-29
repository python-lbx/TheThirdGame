using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magic_Circle_test : MonoBehaviour
{
    public bool NPC;
    public bool Player;
    public GameObject _NPC;
    // Start is called before the first frame update
    
    private void OnEnable() 
    {
        _NPC = gameObject.transform.GetComponentInParent<NPC>().gameObject;

        Invoke("disappear",3f);    
    }

    void disappear()
    {
        if(Player)
        {
            print("Player受到傷害");
        }
        else
        {
            _NPC.GetComponent<NPC>().HP --;
        }

        this.gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Player = true;
        }

        if(other.gameObject.name == "NPC")
        {
            NPC= true;
        }
    }
    

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Player = false;
        }

        if(other.gameObject.name == "NPC")
        {
            NPC= false;
        }
    }
}

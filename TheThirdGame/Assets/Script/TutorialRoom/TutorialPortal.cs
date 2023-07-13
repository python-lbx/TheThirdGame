using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPortal : MonoBehaviour
{
    public bool IsPlayer;
    public GameObject Player;
    public Transform BackToLevel;
    public ClearEquipment clearEquipment;
    public Player_Attributes player_Attributes;
    public PlayerController playerController;

    // Update is called once per frame
    void Update()
    {
        if(IsPlayer)
        {
            if(Input.GetKeyDown(GameManager.GM.interactive))
            {
                playerController.BattleStart = false;
                Player.transform.position = BackToLevel.position;
                clearEquipment.ClearEquip();
                player_Attributes.updateAtt();
            }
        }    
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            IsPlayer = true;
            Player = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            IsPlayer = false;
            Player = null;
        }    
    }
}

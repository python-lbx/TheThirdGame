using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerDate playerdate;

    public static PlayerManager instance;
    private void Awake() 
    {
        //重置
        if(PlayerManager.instance == null)
        {
            PlayerManager.instance = this;
            DontDestroyOnLoad(this);
            playerdate.Date_Reset();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}

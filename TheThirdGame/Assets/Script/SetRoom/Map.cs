using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public bool openMap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(openMap);
        if(Input.GetKeyDown(GameManager.GM.map))
        {
            openMap = !openMap;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(this.gameObject.transform.GetChild(0).gameObject.activeSelf)
            {
                openMap = false;
            }
        }
    }
}

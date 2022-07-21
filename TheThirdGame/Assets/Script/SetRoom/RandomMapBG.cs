using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMapBG : MonoBehaviour
{
    public GameObject[] MapBG;
    public int num;
    // Start is called before the first frame update
    private void Awake() 
    {
        num = Random.Range(0,2);
        switch(num)
        {
            case 0:
            MapBG[0].SetActive(true);
            break;
            case 1:
            MapBG[1].SetActive(true);
            break ;
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

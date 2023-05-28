using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourSideShoot : MonoBehaviour
{
    public GameObject[] ShootList = new GameObject[4];
    public GameObject Player;
    public int i;
    public int j;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            print("C");
            if(i<4)
            {
                ShootList[i].transform.position = Player.transform.position;

                ShootList[i].SetActive(true);

                for(j = 0 ; j < i ; j++)
                {
                    ShootList[j].GetComponent<BulletForTest>().SpawnProjectiles();
                }

                i++;
            }
        }
    }
}

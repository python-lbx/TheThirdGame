using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourSideShoot : MonoBehaviour
{
    public GameObject[] ShootList = new GameObject[4];
    public GameObject Player;
    public int i;
    public int j;

    public float FourSideShootTime;
    public float FourSideShootTimeCD;
    public float deltaTime;

    
    // Start is called before the first frame update

    private void OnEnable() 
    {
        FourSideShootTime = FourSideShootTimeCD;
        i = 0;
        j = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.C))
        // {
        //     print("C");
        //     if(i<4)
        //     {
        //         ShootList[i].transform.position = Player.transform.position;

        //         ShootList[i].SetActive(true);

        //         Invoke("delayshoot",1f);
        //     }
        // }
        
        if(i<4)
        {
            if(FourSideShootTime > 0)
            {
                FourSideShootTime -= Time.deltaTime;
            }
            else if(FourSideShootTime <= 0)
            {

                ShootList[i].transform.position = Player.transform.position;

                ShootList[i].SetActive(true);
                
                Invoke("delayshoot",deltaTime);

                FourSideShootTime = FourSideShootTimeCD;
            }
        }

        if(i == 4)
        {
            this.gameObject.SetActive(false);
        }
    }

    void delayshoot()
    {
        for(j = 0 ; j < i ; j++)
        {
            ShootList[j].GetComponent<BulletForTest>().SpawnProjectiles();
        }

        i++;
    }
}

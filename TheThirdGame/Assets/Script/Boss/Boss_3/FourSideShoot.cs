using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourSideShoot : MonoBehaviour
{
    public GameObject[] ShootList = new GameObject[4];
    public GameObject Player;
    public Animator anim;
    public int i;
    public int j;

    public float FourSideShootTime;
    public float FourSideShootTimeCD;
    public float deltaTime;

    
    // Start is called before the first frame update

    private void OnEnable() 
    {
        foreach(var shoot in ShootList)
        {
            shoot.SetActive(false);
        }
        FourSideShootTime = FourSideShootTimeCD;
        i = 0;
        j = 0;
    }

    private void OnDisable() 
    {
        foreach(var shoot in ShootList)
        {
            shoot.SetActive(false);
        }
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
                anim.SetTrigger("Attack");

                ShootList[i].transform.position = Player.transform.position;

                ShootList[i].SetActive(true);
                
                Invoke("delayshoot",deltaTime);

                FourSideShootTime = FourSideShootTimeCD;
            }
        }

        if(i == 4) //射擊結束
        {
            //取消延遲呼叫
            CancelInvoke("delayshoot");

            //取消顯示
            foreach(var shoot in ShootList)
            {
                shoot.SetActive(false);
            }

            //發射結束不顯示
            this.gameObject.SetActive(false);
        }
    }

    void delayshoot()
    {
        for(j = 0 ; j < i ; j++)
        {
            ShootList[j].GetComponent<ShootTrigger>().SpawnProjectiles();
        }

        i++;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuStyle : MonoBehaviour
{
    public GameObject[] BG;
    public GameObject[] BasicInside;
    public GameObject[] SecondInside;
    public int BGRandomIndex;
    public int InsideRandomIndex;

    public int BGRandomTemp;
    public int InsideRandomTemp;

    public float timer;
    public float timercd;
    // Start is called before the first frame update
    void Start()
    {
        BGRandomIndex = Random.Range(0,BG.Length);
        InsideRandomIndex = Random.Range(0,BasicInside.Length);

        BGRandomTemp = BGRandomIndex;
        InsideRandomTemp = InsideRandomIndex;

        if(BGRandomIndex == 0)
        {
            BG[0].SetActive(true);
            BasicInside[InsideRandomIndex].SetActive(true);
        }
        else if(BGRandomIndex == 1)
        {
            BG[1].SetActive(true);
            SecondInside[InsideRandomIndex].SetActive(true);
        }
    }

    void Update() 
    {
        // if(Input.GetKeyDown(KeyCode.Keypad0))
        // {
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // }
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if(timer <= 0)
        {
            timer = timercd;

            BG[BGRandomTemp].SetActive(false);
            BasicInside[InsideRandomTemp].SetActive(false);
            SecondInside[InsideRandomTemp].SetActive(false);

            BGRandomIndex = Random.Range(0,BG.Length);
            InsideRandomIndex = Random.Range(0,BasicInside.Length);
            
            BGRandomTemp = BGRandomIndex;
            InsideRandomTemp = InsideRandomIndex;

            if(BGRandomIndex == 0)
            {
                BG[0].SetActive(true);
                BasicInside[InsideRandomIndex].SetActive(true);
            }
            else if(BGRandomIndex == 1)
            {
                BG[1].SetActive(true);
                SecondInside[InsideRandomIndex].SetActive(true);
            }

        }
    }
}

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
    // Start is called before the first frame update
    void Start()
    {
        BGRandomIndex = Random.Range(0,BG.Length);
        InsideRandomIndex = Random.Range(0,BasicInside.Length);

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

    // private void Update() 
    // {
    //     if(Input.GetKeyDown(KeyCode.Keypad0))
    //     {
    //         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //     }
    // }
}

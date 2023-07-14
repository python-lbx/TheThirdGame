using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManagerUI : MonoBehaviour
{
    public string levelname;
    public GameObject levelpackage;
    public GameObject settingpackage;
    public GameObject ScreenSetting;

    // Start is called before the first frame update
    void Start()
    {
        levelpackage.SetActive(true);
        settingpackage.SetActive(false); 
        ScreenSetting.SetActive(false);
        AVmanager.instance.Play("Menu");
    }

    // Update is called once per frame
    void Update()
    {   
        if(settingpackage != null && ScreenSetting != null)
        {
            if(settingpackage.activeSelf || ScreenSetting.activeSelf)
            {
                levelpackage.SetActive(false);
            }
            else
            {
                levelpackage.SetActive(true);
            }        
        }



        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(settingpackage.activeSelf)
            {
                settingpackage.SetActive(false);
            }

            if(ScreenSetting.activeSelf)
            {
                ScreenSetting.SetActive(false);
            }
        }
    }
    public void gamestart()
    {
        SceneManager.LoadScene(levelname);
        AVmanager.instance.Stop("Menu");
    }

    public void QTG()
    {
        Application.Quit();
    }

    public void openSetting()
    {
        settingpackage.SetActive(true);
        Time.timeScale = 0;
    }

    public void openScreenSetting()
    {
        ScreenSetting.SetActive(true);
        Time.timeScale = 0;
    }
}

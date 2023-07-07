using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingManager : MonoBehaviour
{
    public GameObject settingpackage;
    public GameObject EscMenu;
    public LevelManagerUI levelManagerUI;
    public string levelname;
    public bool EscMenuActive;

    void Start() 
    {
        settingpackage.SetActive(false);
        EscMenu.SetActive(false);
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name != levelname)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                EscMenuActive = !EscMenuActive;
                EscMenu.SetActive(EscMenuActive);
                if(settingpackage.activeSelf)
                {
                    settingpackage.SetActive(false);
                }
            }
        }

        if(SceneManager.GetActiveScene().name == levelname)
        {
            levelManagerUI = GameObject.Find("LevelManagerUI").GetComponent<LevelManagerUI>();
            levelManagerUI.settingpackage = this.settingpackage;
        }
    }

    public void openSetting()
    {
        settingpackage.SetActive(true);
        if(EscMenu != null)
        {
            EscMenuActive = false;
            EscMenu.SetActive(false);
        }
    }

    public void closeSetting()
    {        
        settingpackage.SetActive(false);
    }
    
    public void backtomenu()
    {            
        EscMenuActive = false;
        EscMenu.SetActive(false);
        
        if(!EscMenu.activeSelf)
        {
            SceneManager.LoadScene(levelname);
        }
    }
}

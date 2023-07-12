using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public GameObject settingpackage;
    public GameObject EscMenu;
    public GameObject GameOverUI;
    public LevelManagerUI levelManagerUI;
    public Slider AudioSlider;
    public AVmanager AVmanager;

    public string levelname;
    public bool EscMenuActive;

    private void Awake() 
    {
        AudioSlider.value = PlayerPrefs.GetFloat("Audio");
        
    }
    void Start() 
    {
        settingpackage.SetActive(false);
        EscMenu.SetActive(false);

        AVmanager.Totalvolume = AudioSlider.value;
    }

    void Update()
    {   
        AVmanager.Totalvolume = AudioSlider.value;

        if(GameOverUI == null)
        {
            GameOverUI = GameObject.Find("GameOverUI");
        }
        else
        {
            if(GameOverUI.activeSelf)
            {
                EscMenuActive = false;
                EscMenu.SetActive(EscMenuActive);
                settingpackage.SetActive(false);
                if(settingpackage.activeSelf)
                {
                    settingpackage.SetActive(false);
                }
            }
        }

        if(SceneManager.GetActiveScene().name == levelname)
        {
            if(levelManagerUI == null)
            {
                levelManagerUI = GameObject.Find("LevelManagerUI").GetComponent<LevelManagerUI>();
            }
            else
            {
                levelManagerUI.settingpackage = this.settingpackage;
            }
        }

        if(GameOverUI != null)
        {
            if(GameOverUI.activeSelf)
            {
                return;
            }
        }

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
            AVmanager.instance.Stop("Level");
            AVmanager.instance.Stop("Boss1");
            AVmanager.instance.Stop("Boss2");
            AVmanager.instance.Stop("Boss3");
            AVmanager.instance.Stop("Tutorial");
            AVmanager.instance.Play("Menu");

            SceneManager.LoadScene(levelname);
            Time.timeScale = 1;
        }
    }

    public void QTG()
    {
        Application.Quit();
    }
}

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
    public GameObject ScreenSetting;
    public Toggle checkmark;
    public LevelManagerUI levelManagerUI;
    public Slider AudioSlider;
    public Dropdown resolutionDropdown;
    public AVmanager AVmanager;

    public string levelname;
    public bool EscMenuActive;

    Resolution[] resolutions;
    [Header("解析度")]
    public int width;
    public int height;
    public int currentResolutionIndex;
    public int fullscreen;
    
    private void Awake() 
    {
        AudioSlider.value = PlayerPrefs.GetFloat("Audio",1f); //必須對應KEY值默認為1
    }
    void Start() 
    {
        settingpackage.SetActive(false);
        EscMenu.SetActive(false);

        AVmanager.Totalvolume = AudioSlider.value;

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for(int i = 0 ; i < resolutions.Length ; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width  == Screen.currentResolution .width &&
               resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }


        resolutionDropdown.AddOptions(options);
        //resolutionDropdown.value = currentResolutionIndex; //下拉式清單中索列等於當前解析度索列
        resolutionDropdown.value = PlayerPrefs.GetInt("currentResolutionIndex",currentResolutionIndex);
        resolutionDropdown.RefreshShownValue(); //刷新下拉式清單的選項

        ////全螢幕設定
        //先讀取0和1的全螢幕值
        fullscreen = PlayerPrefs.GetInt("fullscreen",1);

        //轉成布林值
        bool screenbool = fullscreen == 1? true:false;

        //打勾與否
        checkmark.isOn = screenbool;

        //全螢幕與否
        SetFullscreen(screenbool);
        //print(screenbool);

        ////解析度設定
        width = PlayerPrefs.GetInt("width",Screen.currentResolution.width);
        height = PlayerPrefs.GetInt("height",Screen.currentResolution.height);
        Screen.SetResolution(width,height,screenbool);

    }

    void Update()
    {   
        AVmanager.Totalvolume = AudioSlider.value;

        PlayerPrefs.SetFloat("Audio",AudioSlider.value); //存儲KEY值
        PlayerPrefs.SetInt("width",width);
        PlayerPrefs.SetInt("height",height);
        PlayerPrefs.SetInt("currentResolutionIndex",currentResolutionIndex);


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

        if(SceneManager.GetActiveScene().name == levelname)
        {
            if(levelManagerUI == null)
            {
                levelManagerUI = GameObject.Find("LevelManagerUI").GetComponent<LevelManagerUI>();
            }
            else
            {
                levelManagerUI.settingpackage = this.settingpackage;
                levelManagerUI.ScreenSetting = this.ScreenSetting;
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

                if(ScreenSetting.activeSelf)
                {
                    ScreenSetting.SetActive(false);
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

    public void openScreenSetting()
    {
        ScreenSetting.SetActive(true);
        if(EscMenu != null)
        {
            EscMenuActive = false;
            EscMenu.SetActive(false);
        }
    }

    public void closeSetting()
    {        
        settingpackage.SetActive(false);
        ScreenSetting.SetActive(false);
        Time.timeScale = 1;
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

    // public void SetQuality(int qualityIndex)
    // {
    //     QualitySettings.SetQualityLevel(qualityIndex);
    //     print("qualityIndex:"+qualityIndex); //0~4
    // }

    public void SetFullscreen(bool isFullscreen)
    {
        
        checkmark.isOn = isFullscreen;

        fullscreen = isFullscreen? 1:0;

        PlayerPrefs.SetInt("fullscreen",fullscreen);

        Screen.fullScreen = isFullscreen;

        print(isFullscreen);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);

        width = resolution.width;
        height = resolution.height;

        currentResolutionIndex = resolutionIndex;

        print(resolution.width+ ","+ resolution.height + ","+Screen.fullScreen);
        print(resolutionIndex);
    }
}

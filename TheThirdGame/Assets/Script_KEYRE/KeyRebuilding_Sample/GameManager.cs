using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    public KeyCode left{get;set;}
    public KeyCode right{get;set;}
    public KeyCode up{get;set;}
    public KeyCode down{get;set;}
    public KeyCode jump{get;set;}
    public KeyCode attack{get;set;}
    public KeyCode shuriken{get;set;}
    public KeyCode s_attack{get;set;}
    public KeyCode shield{get;set;}
    public KeyCode dash{get;set;}
    public KeyCode map{get;set;}
    public KeyCode bag{get;set;}
    public KeyCode interactive{get;set;}

    void Awake() 
    {
        //唯一
        if(GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
        else if(GM != this)
        {
            Destroy(gameObject);
        }

        //按鍵預存
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey","A"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey","D"));
        up = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("upKey","W"));
        down = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("downKey","S"));
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey","Space"));
        attack = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("attackKey","Z"));
        shuriken = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("shurikenKey","F"));
        s_attack = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("s_attackKey","X"));
        shield = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("shieldKey","R"));
        dash = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("dashKey","C"));
        map = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("mapKey","M"));
        bag = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("bagKey","B"));
        interactive = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("interactiveKey","Y"));
    }
}

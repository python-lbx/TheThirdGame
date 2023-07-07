using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Transform menuPanel;
    Event keyEvent;
    Text buttonText;
    KeyCode newKey;

    bool waitingForKey;

    // Start is called before the first frame update
    void Start()
    {
        //menuPanel.gameObject.SetActive(false);
        waitingForKey = false;

        //各對應元物件底下的子物件
        for(int i = 0; i < menuPanel.childCount; i++)
        {
            if(menuPanel.GetChild(i).name == "LeftKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.left.ToString();
            }
            else if(menuPanel.GetChild(i).name == "RightKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.right.ToString();
            }
            else if(menuPanel.GetChild(i).name == "UpKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.up.ToString();
            }
            else if(menuPanel.GetChild(i).name == "DownKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.down.ToString();
            }
            else if(menuPanel.GetChild(i).name == "JumpKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.jump.ToString();
            }
            else if(menuPanel.GetChild(i).name == "AttackKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.attack.ToString();
            }
            else if(menuPanel.GetChild(i).name == "ShurikenKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.shuriken.ToString();
            }
            else if(menuPanel.GetChild(i).name == "DashKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.dash.ToString();
            }
            else if(menuPanel.GetChild(i).name == "S_AttackKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.s_attack.ToString();
            }            
            else if(menuPanel.GetChild(i).name == "ShieldKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.shield.ToString();
            }            
            else if(menuPanel.GetChild(i).name == "BagKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.bag.ToString();
            }
            else if(menuPanel.GetChild(i).name == "MapKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.map.ToString();
            }
            else if(menuPanel.GetChild(i).name == "InteractiveKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.interactive.ToString();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //UI控制
    private void OnGUI() 
    {
        keyEvent = Event.current;

        if(keyEvent.isKey && waitingForKey)
        {
            newKey = keyEvent.keyCode;
            waitingForKey = false;
        }
    }

    public void StartAssignment(string keyName)
    {
        if(!waitingForKey)
        {
            StartCoroutine(AssignKey(keyName));
        }
    }


    //顯示按鍵字元
    public void SendText(Text text)
    {
        buttonText = text;
    }

    IEnumerator WaitForKey()
    {
        while(!keyEvent.isKey)
        {
            yield return null;
        }
    }


    //更改按鍵
    public IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;
        yield return WaitForKey();

        switch(keyName)
        {
            case "left":
            GameManager.GM.left = newKey;
            buttonText.text = GameManager.GM.left.ToString();
            PlayerPrefs.SetString("leftKey",GameManager.GM.left.ToString());
            break;

            case "right":
            GameManager.GM.right = newKey;
            buttonText.text = GameManager.GM.right.ToString();
            PlayerPrefs.SetString("rightKey",GameManager.GM.right.ToString());
            break;

            case "up":
            GameManager.GM.up = newKey;
            buttonText.text = GameManager.GM.up.ToString();
            PlayerPrefs.SetString("upKey",GameManager.GM.up.ToString());
            break;

            case "down":
            GameManager.GM.down = newKey;
            buttonText.text = GameManager.GM.down.ToString();
            PlayerPrefs.SetString("downKey",GameManager.GM.down.ToString());
            break;

            case "jump":
            GameManager.GM.jump = newKey;
            buttonText.text = GameManager.GM.jump.ToString();
            PlayerPrefs.SetString("jumpKey",GameManager.GM.jump.ToString());
            break;

            case "attack":
            GameManager.GM.attack = newKey;
            buttonText.text = GameManager.GM.attack.ToString();
            PlayerPrefs.SetString("attackKey",GameManager.GM.attack.ToString());
            break;

            case "shuriken":
            GameManager.GM.shuriken = newKey;
            buttonText.text = GameManager.GM.shuriken.ToString();
            PlayerPrefs.SetString("shurikenKey",GameManager.GM.shuriken.ToString());
            break;

            case "dash":
            GameManager.GM.dash = newKey;
            buttonText.text = GameManager.GM.dash.ToString();
            PlayerPrefs.SetString("dashKey",GameManager.GM.dash.ToString());
            break;
            
            case "s_attack":
            GameManager.GM.s_attack = newKey;
            buttonText.text = GameManager.GM.s_attack.ToString();
            PlayerPrefs.SetString("s_attackKey",GameManager.GM.s_attack.ToString());
            break;

            case "shield":
            GameManager.GM.shield = newKey;
            buttonText.text = GameManager.GM.shield.ToString();
            PlayerPrefs.SetString("shieldKey",GameManager.GM.shield.ToString());
            break;

            case "map":
            GameManager.GM.map = newKey;
            buttonText.text = GameManager.GM.map.ToString();
            PlayerPrefs.SetString("mapKey",GameManager.GM.map.ToString());
            break;

            case "bag":
            GameManager.GM.bag = newKey;
            buttonText.text = GameManager.GM.bag.ToString();
            PlayerPrefs.SetString("bagKey",GameManager.GM.bag.ToString());
            break;
            
            case "interactive":
            GameManager.GM.interactive = newKey;
            buttonText.text = GameManager.GM.interactive.ToString();
            PlayerPrefs.SetString("interactiveKey",GameManager.GM.interactive.ToString());
            break;
        }

        yield return null;
    }

    public void ResetKey(string keyName)
    {
        waitingForKey = true;

        switch(keyName)
        {
            case "left":
            GameManager.GM.left = KeyCode.A;
            buttonText.text = GameManager.GM.left.ToString();
            PlayerPrefs.SetString("leftKey",GameManager.GM.left.ToString());
            break;

            case "right":
            GameManager.GM.right = KeyCode.D;
            buttonText.text = GameManager.GM.right.ToString();
            PlayerPrefs.SetString("rightKey",GameManager.GM.right.ToString());
            break;

            case "up":
            GameManager.GM.up = KeyCode.W;
            buttonText.text = GameManager.GM.up.ToString();
            PlayerPrefs.SetString("upKey",GameManager.GM.up.ToString());
            break;

            case "down":
            GameManager.GM.down = KeyCode.S;
            buttonText.text = GameManager.GM.down.ToString();
            PlayerPrefs.SetString("downKey",GameManager.GM.down.ToString());
            break;

            case "jump":
            GameManager.GM.jump = KeyCode.Space;
            buttonText.text = GameManager.GM.jump.ToString();
            PlayerPrefs.SetString("jumpKey",GameManager.GM.jump.ToString());
            break;

            case "attack":
            GameManager.GM.attack = KeyCode.Z;
            buttonText.text = GameManager.GM.attack.ToString();
            PlayerPrefs.SetString("attackKey",GameManager.GM.attack.ToString());
            break;

            case "shuriken":
            GameManager.GM.shuriken = KeyCode.F;
            buttonText.text = GameManager.GM.shuriken.ToString();
            PlayerPrefs.SetString("shurikenKey",GameManager.GM.shuriken.ToString());
            break;

            case "dash":
            GameManager.GM.dash = KeyCode.C;
            buttonText.text = GameManager.GM.dash.ToString();
            PlayerPrefs.SetString("dashKey",GameManager.GM.dash.ToString());
            break;

            case "s_attack":
            GameManager.GM.s_attack = KeyCode.X;
            buttonText.text = GameManager.GM.s_attack.ToString();
            PlayerPrefs.SetString("s_attackKey",GameManager.GM.s_attack.ToString());
            break;

            case "shield":
            GameManager.GM.shield = KeyCode.R;
            buttonText.text = GameManager.GM.shield.ToString();
            PlayerPrefs.SetString("shieldKey",GameManager.GM.shield.ToString());
            break;

            case "map":
            GameManager.GM.map = KeyCode.M;
            buttonText.text = GameManager.GM.map.ToString();
            PlayerPrefs.SetString("mapKey",GameManager.GM.map.ToString());
            break;

            case "bag":
            GameManager.GM.bag = KeyCode.B;
            buttonText.text = GameManager.GM.bag.ToString();
            PlayerPrefs.SetString("bagKey",GameManager.GM.bag.ToString());
            break;


            case "interactive":
            GameManager.GM.interactive = KeyCode.Y;
            buttonText.text = GameManager.GM.interactive.ToString();
            PlayerPrefs.SetString("interactiveKey",GameManager.GM.interactive.ToString());
            break;
        }

        waitingForKey = false;
    }
}

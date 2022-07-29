using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResourceUI : MonoBehaviour
{
    public PlayerController playercontroller;
    public Image HPBar;
    public Text HPText;
    public Image[] MPBall;
    // Start is called before the first frame update
    void Start()
    {
        HPText.text = playercontroller.CurrentHP.ToString() + "/" + playercontroller.HP.ToString();
        print(MPBall.Length);
    }

    // Update is called once per frame
    void Update()
    {
        HPText.text = playercontroller.CurrentHP.ToString() + "/" + playercontroller.HP.ToString();
        HPBar.fillAmount = playercontroller.CurrentHP/playercontroller.HP;

        for(int i = 0 ; i < MPBall.Length ; i++) //MPBall.Length = 5;
        {
            if(i < playercontroller.MPBall) // 0:0 false:false 0:1 
            {
                MPBall[i].enabled = true;
            }
            else
            {
                MPBall[i].enabled = false;
            }
        }
        
    }
}

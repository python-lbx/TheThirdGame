using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CDT : MonoBehaviour
{
    public float CR;
    public float CDR;
    public int damage;
    public Text CR_Text;
    public Text CDR_Text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CR_Text.text = CR.ToString();
        CDR_Text.text = (100+CDR).ToString(); //100 + 5 = 105 面板顯示暴擊傷害為105%
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        float C_Damage = damage * (1 + (CDR/100)); // (1+ 5/100) = 1.05 暴擊傷害為105%
        if(other.gameObject.name == "Enemy")
        {
            print(Random.value);
            if(Random.value < (CR/100)) // 5/100 = 0.05 暴擊率為5%
            {
                print("暴擊!造成傷害:" + C_Damage);
            }
            else
            {
                print("造成傷害:" + damage);
            }
        }    
    }
}

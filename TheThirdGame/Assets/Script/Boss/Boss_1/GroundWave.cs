using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundWave : MonoBehaviour
{
    
    public GameObject[] Wave;
    public bool[] Shooted;
    public float NumOfWave;
    public float WaveCD;
    // Start is called before the first frame update
    void Start()
    {   

        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Ground_Wave()
    {
        if(WaveCD > 0)
        {
            WaveCD -= Time.deltaTime;
        }
        else if(WaveCD <= 0 && NumOfWave < 4)
        {
            for(var j =0 ; j < Shooted.Length ; j++)
            {
                var num = Random.Range(0,4);
                if(Shooted[num] != true)
                {                    
                    print(num);
                    Shooted[num] = true;
                    Wave[num].GetComponent<Tilemap>().color = Color.red;
                    StartCoroutine(DelayWave(Wave[num]));
                    WaveCD = 0.75f;
                    NumOfWave ++;
                    break;
                }
            }
        }    

        if(NumOfWave == 4) //技能次數達到四時
        {
            for(var i = 0 ; i < Shooted.Length ; i++)
            {
                Shooted[i] = false; //進入待機
            }
        }    
    }

    IEnumerator DelayWave(GameObject obj)
    {
        yield return new WaitForSeconds(3f);
        AVmanager.instance.Play("Wizard_FireSpell_3");
        obj.transform.GetChild(0).gameObject.SetActive(true);
    }
}

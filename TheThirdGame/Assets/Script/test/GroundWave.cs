using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundWave : MonoBehaviour
{
    
    public GameObject[] Wave;
    public bool[] Shooted;
    public float time;
    public float wavetime;
    // Start is called before the first frame update
    void Start()
    {   

        
    }

    // Update is called once per frame
    void Update()
    {
        if(wavetime > 0)
        {
            wavetime -= Time.deltaTime;
        }
        else if(wavetime <= 0 && time < 4)
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
                    wavetime = 0.75f;
                    time ++;
                    break;
                }
            }
        }
    }

    IEnumerator DelayWave(GameObject obj)
    {
        yield return new WaitForSeconds(3f);
        obj.transform.GetChild(0).gameObject.SetActive(true);
    }
}

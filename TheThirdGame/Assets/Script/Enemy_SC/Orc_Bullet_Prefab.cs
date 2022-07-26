using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Bullet_Prefab : MonoBehaviour
{
    public float activeTime;
    public float activeStart;
    // Start is called before the first frame update

    private void OnEnable() 
    {
        activeStart = Time.time;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= activeStart + activeTime) //生成時間過後消失
        {
            Orc_Bullet_Pool.instance.ReturnPool(this.gameObject);
        }    
    }
}

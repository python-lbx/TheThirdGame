using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameWave : MonoBehaviour
{
    public float damage;
    public float activeTime;
    public float activeStart;
    private void OnEnable()
    {
        activeStart = Time.time;
    }
    // Start is called before the first frame update
    void Update()
    {
        if(Time.time >= activeStart + activeTime) //生成時間過後消失
        {
            MoveWave_Pool.instance.ReturnPool(this.gameObject);
        }    
    }
}

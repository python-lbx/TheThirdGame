using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForCount : MonoBehaviour
{   
    public float radius = 5f; // 圓的半徑
    public Vector2 Direction;
    public Vector2 targetpos;
    public Vector2 origin; // 圓心
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Direction = targetpos - origin; // 計算方向向量，以圓心為參考點 (3,4)-(0,0) = (3,4) 坐標值
        print(Direction.normalized +"方向向量");

        Vector2 circlePos = origin + radius * Direction.normalized;
        print(circlePos + "點在圓的位置");
    }
}

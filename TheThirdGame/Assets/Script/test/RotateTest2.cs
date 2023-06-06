using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTest2 : MonoBehaviour
{
    
    [Header("攻擊目標")]
    public GameObject Target;
    public GameObject shootPoint;
    public GameObject originObject; // 圓心 object
    public GameObject Laser;
    public float radius = 5f; // 圓的半徑
    Vector2 Direction;
    Vector2 targetpos;
    Vector2 origin; // 圓心
    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        origin = originObject.transform.position; // 取得圓心位置    
    }

    // Update is called once per frame
    void Update()
    {
        targetpos = Target.transform.position;

        Direction = targetpos - origin; // 計算方向向量，以圓心為參考點 (3,4)-(0,0) = (3,4) 坐標值

        shootPoint.transform.up = Direction.normalized; // 瞄準目標 Direction.normalized = 根號(3^2+4^5) = 5 得出  (3/5, 4/5),長度為1的方向向量

        // 計算 shootPoint 在圓上的位置
        Vector2 circlePos = origin + radius * Direction.normalized; //
        float distance = Direction.magnitude; // 使用向量長度計算距離
        if (distance < radius)
        {
            circlePos = origin + Direction.normalized * radius;
        }
        shootPoint.transform.position = circlePos;

        if(Input.GetKeyDown(KeyCode.Keypad5))
        {
            var obj = Instantiate(Laser,shootPoint.transform.position,Quaternion.identity);
            obj.transform.up = Direction;
        }
    }
}
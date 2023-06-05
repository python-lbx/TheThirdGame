using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTest : MonoBehaviour
{
    public float radius; // 圓形運動的半徑
    public float distanceBD; // 瞄準點與發射點距離
    public Transform centerPoint; // 旋轉中心點
    public Transform AimPoint; // 瞄準點
    public Transform ShootPoint; // 發射點
    public Transform Target; // 瞄準目標


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        Vector3 directionBC = (Target.position - transform.position).normalized; // 計算BC向量
        ShootPoint.position = transform.position + directionBC * distanceBD; 

        Vector3 directionAC = Target.position - centerPoint.position; // 計算AC向量

        Vector3 directionAB = directionAC.normalized; // 計算AB向量

        float distanceAB = Vector3.Distance(centerPoint.position, Target.position); // 計算AB距離
        Vector3 positionB = centerPoint.position + directionAB * distanceAB * 0.5f; // 計算物件B的位置

        transform.position = 
        new Vector3
        (
            centerPoint.position.x + (positionB - centerPoint.position).normalized.x * radius, 
            centerPoint.position.y + (positionB - centerPoint.position).normalized.y * radius, 
            transform.position.z
        ); // 設定物件B的位置

        Vector3 directionBD = (AimPoint.position - ShootPoint.position).normalized; // 計算BD向量
        Vector3 positionD = AimPoint.position - directionBD * distanceBD; // 計算物件D的位置
        ShootPoint.position = new Vector3(positionD.x, positionD.y, transform.position.z); // 設定物件D的位置


        Vector3 aimDirection = Target.position - transform.position; // 計算瞄準方向
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg; // 計算角度
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle)); // 設定旋轉    

        ShootPoint.transform.up = aimDirection;
        ShootPoint.Rotate(0,0,ShootPoint.rotation.z);        
    }   
}

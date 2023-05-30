using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet_Pool : MonoBehaviour
{
    public static ShootBullet_Pool instance;
    public GameObject BulletPrefab;
    public int PrefabCount;
    Queue<GameObject> availableObjects = new Queue<GameObject>();

    private void Awake()
    {
        instance = this;

        FillPool();
    }
    public void FillPool()
    {
        for(int i = 0 ; i < PrefabCount ; i++)
        {
            var newPrefab = Instantiate(BulletPrefab);
            newPrefab.transform.SetParent(transform);

            ReturnPool(newPrefab);
        }
    }

    public void ReturnPool(GameObject gameObject)
    {
        gameObject.SetActive(false);
        availableObjects.Enqueue(gameObject);
    }
    public GameObject GetFormPool(Transform point) //在目標位置生成
    {
        if(availableObjects.Count == 0)
        {
            FillPool();
        }


        var outPrefab = availableObjects.Dequeue();

        outPrefab.SetActive(true);
        outPrefab.transform.position = point.position;

        return outPrefab;
    }   

}

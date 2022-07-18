using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatDamagePool : MonoBehaviour
{
    public static FloatDamagePool instance;
    public GameObject floatdamagePrefab;
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
            var newPrefab = Instantiate(floatdamagePrefab);
            newPrefab.transform.SetParent(transform);

            ReturnPool(newPrefab);
        }
    }

    public void ReturnPool(GameObject gameObject)
    {
        gameObject.SetActive(false);
        availableObjects.Enqueue(gameObject);
    }

    public GameObject GetFormPool() //在目標位置生成
    {
        if(availableObjects.Count == 0)
        {
            FillPool();
        }


        var outPrefab = availableObjects.Dequeue();

        outPrefab.SetActive(true);

        return outPrefab;
    }   
}

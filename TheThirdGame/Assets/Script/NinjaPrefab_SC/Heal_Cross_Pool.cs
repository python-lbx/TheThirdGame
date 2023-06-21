using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal_Cross_Pool : MonoBehaviour
{
    public static Heal_Cross_Pool instance;
    public GameObject heal_cross_prefab;
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
            var newPrefab = Instantiate(heal_cross_prefab);
            newPrefab.transform.SetParent(transform);

            ReturnPool(newPrefab);
        }
    }

    public void ReturnPool(GameObject gameObject)
    {
        heal_cross_prefab.GetComponent<Heal_Cross>().obj = null;
        gameObject.SetActive(false);
        availableObjects.Enqueue(gameObject);
    }
    
    public GameObject GetFormPool(Transform point, GameObject obj) //在目標位置生成
    {
        //heal_cross_prefab.GetComponent<Heal_Cross>().o = obj;

        if(availableObjects.Count == 0)
        {
            FillPool();
        }

        //這個才是生成物
        var outPrefab = availableObjects.Dequeue();

        outPrefab.SetActive(true);
        outPrefab.GetComponent<Heal_Cross>().obj = obj;
        outPrefab.transform.position = point.position;

        return outPrefab;
    }    
}

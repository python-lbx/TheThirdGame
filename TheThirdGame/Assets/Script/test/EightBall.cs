using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EightBall : MonoBehaviour
{
    public GameObject[] ballpoint= new GameObject[8];
    public int[] numbers = { 0, 1, 2, 3 , 4 , 5 , 6 , 7};
    public int i;
    public float activetime;
    public float activetimecd;

    // Start is called before the first frame update
    private void OnEnable() 
    {
        print("E");
        shuffleArray(numbers);
        Debug.Log(string.Join(", ", numbers));

        i = 0;
        activetime = activetimecd;
        print(ballpoint[numbers[i]]);
    }

    private void OnDisable() 
    {
        foreach(var ball in ballpoint)
        {
            ball.GetComponent<Energy_Ball>().ResPos(); //重置位置
            ball.SetActive(false); //上一個迴圈的球顯示要關掉 很重要
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(i < numbers.Length)
        {
            if(activetime > 0)
            {
                activetime -= Time.deltaTime;
            }
            else if(activetime <= 0)
            {   
                print(ballpoint[numbers[i]]);
                ballpoint[numbers[i]].SetActive(true);
                activetime = activetimecd;
                i++;
            }
        }
    }

    void shuffleArray<T>(T[] array)
    {
        for (int j = 0; j < array.Length; j++) 
        {
            int randomIndex = Random.Range(j, array.Length);
            T temp = array[j];
            array[j] = array[randomIndex];
            array[randomIndex] = temp;
        }    
    }
}

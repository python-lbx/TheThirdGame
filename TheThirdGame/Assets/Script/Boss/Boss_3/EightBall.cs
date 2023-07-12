using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EightBall : MonoBehaviour
{
    public GameObject[] ballpoint= new GameObject[8];
    public int[] numbers = { 0, 1, 2, 3 , 4 , 5 , 6 , 7};
    public int i;
    public float activetime;
    public float activetimecd;
    
    [Header("對話框")]
    public GameObject DialogTable;
    public GameObject TextPoint;
    public Text Dialog;

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
        print("D");
        foreach(var ball in ballpoint)
        {
            ball.GetComponent<Energy_Ball>().ResPos(); //重置位置
            ball.SetActive(false); //上一個迴圈的球顯示要關掉 很重要
        }

        shuffleArray(numbers);
        Debug.Log(string.Join(", ", numbers));

        i = 0;
        activetime = activetimecd;
        print(ballpoint[numbers[i]]);
    }

    // Update is called once per frame
    void Update()
    {
                //對話框位置
        DialogTable.transform.position = TextPoint.transform.position;

        if(i < numbers.Length)
        {
            if(activetime > 0)
            {
                activetime -= Time.deltaTime;
            }
            else if(activetime <= 0)
            {   
                DialogTable.SetActive(false);
                Dialog.text = "";
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall_Random_Color : MonoBehaviour
{
    public int[] numbers = { 0, 1, 2 };
    public GameObject[] magicball;
    // Start is called before the first frame update

    private void OnEnable() 
    {
        shuffleArray(numbers); //洗牌
        Debug.Log(string.Join(", ", numbers)); //新序列
        
        // magicball[0].GetComponent<SpriteRenderer>().color = Color.red;
        // magicball[1].GetComponent<SpriteRenderer>().color = Color.yellow;
        // magicball[2].GetComponent<SpriteRenderer>().color = Color.green;

        for (int i = 0; i < numbers.Length; i++) 
        {
            if(numbers[i] == 0)
            {
                magicball[i].GetComponent<SpriteRenderer>().color = Color.red;
                magicball[i].GetComponent<MagicBall>().Key = "Red";
            }
            else if(numbers[i] == 1)
            {
                magicball[i].GetComponent<SpriteRenderer>().color = Color.yellow;
                magicball[i].GetComponent<MagicBall>().Key = "Yellow";
            }
            else if(numbers[i] == 2)
            {
                magicball[i].GetComponent<SpriteRenderer>().color = Color.green;
                magicball[i].GetComponent<MagicBall>().Key = "Green";
            }
        }
    }

    public void shuffleArray<T>(T[] array)
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

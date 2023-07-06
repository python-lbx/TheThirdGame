using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public string levelname;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void gamestart()
    {
        SceneManager.LoadScene(levelname);
    }

    public void QTG()
    {
        Application.Quit();
    }
}

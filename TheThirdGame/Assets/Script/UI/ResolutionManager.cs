using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    public int width;
    public int height;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(width,height,true);
    }

    // Update is called once per frame
    void Update()
    {
    }
}

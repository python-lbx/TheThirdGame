using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part_Attribute : MonoBehaviour
{
    public EquipBoxData Part;
    public string PartName;
    public int PartHP;
    public int PartATK;
    public int PartDEF;
    public int PartSPEED;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PartName = Part.whichPart;
        PartHP = Part.HP;
        PartATK = Part.ATK;
        PartDEF = Part.DEF;
        PartSPEED = Part.Speed;
    }
}

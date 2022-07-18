using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part_Attribute : MonoBehaviour
{
    public EquipBoxData Part;
    public string PartName;
    public int PartHP;
    public int PartATK;
    public int PartCRI;
    public int PartCSD;
    public int PartSPD;
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
        PartCRI = Part.CRI;
        PartCSD = Part.CSD;
        PartSPD = Part.SPD;
    }
}

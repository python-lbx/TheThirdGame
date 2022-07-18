using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character",menuName = "")]
public class CharacterData : ScriptableObject
{
    public enum TypeofCharactor{ememy,player};
    public TypeofCharactor typeofCharactor;

    [Header("屬性")]
    public int CurrentHP;
    public int MaxHP;
    public int ATK;
    public float CRI;
    public float CSD;
    public int SPD;

    [Header("初始值")]
    public int D_MaxHP;
    public int D_ATK;
    public float D_CRI;
    public float D_CSD;
    public int D_SPD;
    public int D_KillPoint;
    public int D_MaxExp;
    
    [Header("經驗值")]
    public int KillPoint;
    public int BaseExp;
    public int MaxExp;
    public int CurrentLevel;
    public int MaxLevel;
    public float LevelBuff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Data_Reset()
    {
        MaxHP = D_MaxHP;
        CurrentHP = MaxHP;
        ATK = D_ATK;
        CRI = D_CRI;
        SPD = D_SPD;
        KillPoint = D_KillPoint;
        BaseExp = 0;
        MaxExp = D_MaxExp;
        CurrentLevel = 1;
    }

    public float LevelMultiplier
    {
        get { return 1 + (CurrentLevel -1) * LevelBuff;}
    }
    

    public void UpdateExp(int KillPoint)
    {
        BaseExp += KillPoint;

        if(BaseExp >= MaxExp)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        CurrentLevel = Mathf.Clamp(CurrentLevel + 1,0,MaxLevel);

        MaxExp += (int)(MaxExp * LevelMultiplier); // 100+ 110

        MaxHP = (int)(MaxHP * LevelMultiplier); // 110
        CurrentHP = MaxHP;
        BaseExp = 0;

        Debug.Log("升級了!!當前等級" + CurrentLevel + 
                  "生命值" + MaxHP +
                  "下一級還需" + MaxExp);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character",menuName = "")]
public class EnemyData : ScriptableObject
{
    [Header("屬性")]
    public int CurrentHP;
    public int MaxHP;
    public int ATK;
    public int SPD;

    [Header("強度修正")]
    public float CurrentLevel; //當前步數
    public float LevelBuff;

    public float LevelMultiplier
    {
        get { return 1 + (CurrentLevel -1) * LevelBuff;}
    }

    public void LevelUp()
    {
        MaxHP = (int)(MaxHP * LevelMultiplier); // 110
        ATK = (int)(ATK * LevelMultiplier);
        CurrentHP = MaxHP;
    }
}

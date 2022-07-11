using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDate",menuName = "Player/PlayerDate")]
public class PlayerDate : ScriptableObject
{
    [Header("生命值")]
    public int CurrentHP;
    public int MaxHP;

    [Header("屬性")]
    public int AttackPower;
    public int DefensePower;
    public int speed;

    [Header("Kill")]
    public int Killpoint;

    [Header("經驗值")]
    public int CurrentLevel;
    public int MaxLevel;
    public int BaseExp;
    public int CurrentExp;
    public float LevelBuff;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateExp(int point)
    {

    }
    
    public void Date_Reset()
    {
        MaxHP = 100;
        CurrentHP = MaxHP;
        AttackPower = 10;
        DefensePower = 5;
        Killpoint = 0;
        CurrentLevel = 1;
        MaxLevel = 30;
        BaseExp = 50;
        CurrentExp = 0;
        LevelBuff = 0.1f;
        speed = 5;
    }
}

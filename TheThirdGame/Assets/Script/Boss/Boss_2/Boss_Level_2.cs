using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Level_2 : MonoBehaviour
{
    [Header("當前階段")]
    public Statue current_Statue;
    public float PhaseTime;
    public enum Statue{Idle,WalkAndWave,WalkAndFire,Teleport,Laser}

    [Header("下個技能階段")]
    public int SkillPhase;
    public int SkillTime;
    public float SKillCD;
    public Statue Next_Skill_Statue;
    public bool canShoot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //待機
        //移動
        //地熱波
        //待機
        //移動
        //烽光連城
        //待機
        //移動
        //地熱波
        //待機
        //傳送中間
        //毀滅死光
        //傳送地面
        //待機
        //迴圈繼續
        switch (SkillPhase)
        {
            case 0:
            Next_Skill_Statue = Statue.WalkAndWave;
            break;
            
            case 1:
            Next_Skill_Statue = Statue.WalkAndFire;
            break;

            case 2:
            Next_Skill_Statue = Statue.WalkAndWave;
            break;

            case 3:
            Next_Skill_Statue = Statue.Laser;
            break;
        }

        switch(current_Statue)
        {
            case Statue.Idle:
            if(PhaseTime > 0)
            {
                PhaseTime -= Time.deltaTime;
            }
            else if(PhaseTime <= 0)
            {
                PhaseTime = 2f;
                SKillCD = 2f;
                SkillTime = 0;
                current_Statue = Next_Skill_Statue;
            }
            break;

            case Statue.WalkAndWave:
            if(SKillCD > 0)
            {
                SKillCD -= Time.deltaTime;
            }
            else if(SKillCD < 0)
            {
                SkillTime++;
                SKillCD = 2f;
            }

            if(SkillTime == 3)
            {
                SkillPhase++;
                current_Statue = Statue.Idle;
            }
            break;

            case Statue.WalkAndFire:
            if(SKillCD > 0)
            {
                SKillCD -= Time.deltaTime;
            }
            else if(SKillCD < 0)
            {
                SkillTime++;
                SKillCD = 2f;
            }

            if(SkillTime == 4)
            {
                SkillPhase++;
                current_Statue = Statue.Idle;
            }
            break;
            
            case Statue.Teleport:
            break;

            case Statue.Laser:
            break;

        }
    }
}

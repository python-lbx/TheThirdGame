using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("角色屬性")]
    public float HP;
    public float CurrentHP;
    public int MPBall;
    public float ATK;
    public float CRI;
    public float CSD;

    public bool BattleStart;
    //public bool isCrit;
    //public float rate;
    //[Header("面板傷害")]
    //public int damage;
    //public float CRI_Damage;

    /*[Header("UI插件")]
    public Text CR_Text;
    public Text CDR_Text;*/

    //[Header("目標物件")]
    //public GameObject targetpos;
    [SerializeField]
    Player_Attributes CharacterState;
    [SerializeField]
    Animator anim;

    // Start is called before the first frame update
    private void Awake() 
    {
        HP = CharacterState.Player_HP;
        CurrentHP = HP;
    }
    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!BattleStart)
        {
        HP = CharacterState.Player_HP;
        ATK = CharacterState.Player_ATK;
        CRI = CharacterState.Player_CRI;
        CSD = CharacterState.Player_CSD;
        CurrentHP = HP; //準備戰鬥
        }

        HP = CharacterState.Player_HP;
        ATK = CharacterState.Player_ATK;
        CRI = CharacterState.Player_CRI;
        CSD = CharacterState.Player_CSD;
    }

    public void GetDamage(float damage)
    {   
        
        BattleStart = true;
        CurrentHP = Mathf.Clamp(CurrentHP - damage,0,HP);
        anim.SetTrigger("IsHurting");
    }

    public void GetHeal(float heal)
    {
        CurrentHP = Mathf.Clamp(CurrentHP + heal,0,HP);
    }
}

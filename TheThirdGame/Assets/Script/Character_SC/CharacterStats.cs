using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterData tempDate;
    public CharacterData Character;    

    public bool isPlayer;
    CharacterStats isme;

   // public GameObject sword;


    private void Awake() 
    {
        //重置
        isme = this;

        if(tempDate != null)
        {
            tempDate.Data_Reset();
            Character = Instantiate(tempDate);
        }


    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {            

    }
    /*public void TakeDamage(CharacterStats attacker, CharacterStats defener)
    {
        int damage = Mathf.Max(attacker.Character.AttackPower - defener.Character.Defense,0);
        Character.CurrentHP = Mathf.Max(Character.CurrentHP - damage,0);

        if(isme.Character.CurrentHP <=0)
        {
            if(!isPlayer)
            {
            attacker.Character.UpdateExp(Character.KillPoint);

            FindObjectOfType<EnemyController>().lootRate(transform.position);

            Destroy(gameObject);
            }

            Destroy(isme.gameObject);
            

        }
    }


    

    private void OnCollisionEnter2D(Collision2D other) 
    {
        var target = other.gameObject.GetComponent<CharacterStats>();

        target.TakeDamage(isme,target);


        var Critical_Hit_Damage = Character.AttackPower * 1.5f;

        print(Random.value);
        
        if(Random.value < Character.CriticalRate)
        {   
            print(Critical_Hit_Damage);

            print("暴擊!造成傷害:" + Critical_Hit_Damage);
        }
        else
        {   
            print("造成傷害" + Character.AttackPower);
        }
       // print("攻擊者HP" + isme.Character.CurrentHP + "防御者HP" +target.Character.CurrentHP);
        
    }
    */

}

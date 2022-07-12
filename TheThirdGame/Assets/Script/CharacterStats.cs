using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterData tempDate;
    public CharacterData Character;
    public Item item;
    CharacterStats isme;

    public GameObject sword;


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
    public void TakeDamage(CharacterStats attacker, CharacterStats defener)
    {
        int damage = Mathf.Max(attacker.Character.AttackPower - defener.Character.Defense,0);
        Character.CurrentHP = Mathf.Max(Character.CurrentHP - damage,0);

        if(isme.Character.CurrentHP <=0)
        {
            attacker.Character.UpdateExp(isme.Character.KillPoint);
            if(Random.value < item.DropRate)
            {
                Instantiate(sword,transform.position,Quaternion.identity);
            }
            Destroy(isme.gameObject);
        }
    }
    

    private void OnCollisionEnter2D(Collision2D other) 
    {
        var target = other.gameObject.GetComponent<CharacterStats>();

        target.TakeDamage(isme,target);


       // print("攻擊者HP" + isme.Character.CurrentHP + "防御者HP" +target.Character.CurrentHP);
        
    }

}

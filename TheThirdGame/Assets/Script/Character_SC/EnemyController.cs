using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("掉寶")]
    public LootArray loot;
    [Header("資料庫")]
    public EnemyData tempDate; //正本
    public EnemyData thisData; //副本
    [Header("角色屬性")]
    public bool IsBoss;
    public float health;
    public float currenthealth;
    public float ATK;
    public bool dead;
    [Header("房間信息")]
    public Room whichroom;

    private void OnEnable() 
    {
        if(tempDate != null && !IsBoss)
        {
            thisData = Instantiate(tempDate);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(!IsBoss)
        {
        thisData.CurrentLevel = whichroom.roomDirecter.RoomLevel; //當前房間等級
        thisData.LevelUp();
        currenthealth = health = thisData.MaxHP;
        ATK = Mathf.Round(thisData.ATK);

        whichroom.totalHP += thisData.MaxHP;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsBoss && currenthealth <= 0)
        {   
            if(whichroom != null)
            {
                if(!dead)
                {
                    whichroom.dead(health); //房間總血量耗損
                    lootRate(this.transform.position);
                    dead = true;
                }
            }


            //Destroy(this.gameObject);
        }
    }

    public void lootRate(Vector2 _me)
    {
        float num = UnityEngine.Random.value;
        for(var i = 0 ; i < loot.lootarray.Length ; i++)
        {
            //print(loot.lootarray[i].GetComponent<ItemOnWorld>().tempDate.DropRate);

            if(num < loot.lootarray[i].GetComponent<ItemOnWorld>().tempDate.DropRate)
            {
                print("num" + num);
                print("rate" + loot.lootarray[i].GetComponent<ItemOnWorld>().tempDate.DropRate + loot.lootarray[i].GetComponent<ItemOnWorld>().tempDate.ItemName);
                Instantiate(loot.lootarray[i], _me ,Quaternion.identity);
                break;
            }
        }
    }

    /*private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(whichroom != null)
            {
                whichroom.dead(health); //房間總血量耗損
            }
            //health -=1;

            //Destroy(this.gameObject);
        }
    }*/

    public void GetDamage(float damage)
    {
        currenthealth = Mathf.Clamp(currenthealth - damage,0,health);
    }

    public void GetHeal(float heal)
    {
        currenthealth = Mathf.Clamp(currenthealth + heal,0,health);
    }
}

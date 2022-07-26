using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public LootArray loot;
    public EnemyData tempDate; //正本
    public EnemyData thisData; //副本
    public float health;
    public float currenthealth;
    public Room whichroom;
    public int RoomStep;

    private void OnEnable() 
    {
        if(tempDate != null)
        {
            thisData = Instantiate(tempDate);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
            thisData.CurrentLevel = whichroom.roomDirecter.roomClear;//當前房間步數
            thisData.LevelUp();
            currenthealth = health = thisData.MaxHP;

    }

    // Update is called once per frame
    void Update()
    {
        if(currenthealth <= 0)
        {   
            if(whichroom != null)
            {
                whichroom.dead(health); //房間總血量耗損
            }
            lootRate(this.transform.position);
            Destroy(this.gameObject);
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
        currenthealth -= damage;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossResourceUI : MonoBehaviour
{    
    public boss Boss; //新的
    public enum boss {One,Two,Three}; //新的
    public EnemyController[] enemyController;
    public Image HPBar;
    public Text HPText;

    // Start is called before the first frame update
    void Start()
    {
        switch(Boss)
        {
            case boss.One:
                HPText.text = enemyController[0].currenthealth.ToString() + "/" + enemyController[0].health.ToString();
            break;

            case boss.Two:
                HPText.text = enemyController[1].currenthealth.ToString() + "/" + enemyController[1].health.ToString();
            break;

            case boss.Three:
                HPText.text = enemyController[2].currenthealth.ToString() + "/" + enemyController[2].health.ToString();
            break;
        }

        //原始
        //HPText.text = enemyController.currenthealth.ToString() + "/" + enemyController.health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        switch(Boss)
        {
            case boss.One:
                HPText.text = enemyController[0].currenthealth.ToString() + "/" + enemyController[0].health.ToString();
                HPBar.fillAmount = enemyController[0].currenthealth/enemyController[0].health;
            break;

            case boss.Two:
                HPText.text = enemyController[1].currenthealth.ToString() + "/" + enemyController[1].health.ToString();
                HPBar.fillAmount = enemyController[1].currenthealth/enemyController[1].health;
            break;

            case boss.Three:
                HPText.text = enemyController[2].currenthealth.ToString() + "/" + enemyController[2].health.ToString();
                HPBar.fillAmount = enemyController[2].currenthealth/enemyController[2].health;
            break;
        }

        //原始
        // HPText.text = enemyController.currenthealth.ToString() + "/" + enemyController.health.ToString();
        // HPBar.fillAmount = enemyController.currenthealth/enemyController.health;
    }
}
